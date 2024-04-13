using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rihal.ReelRise.Application.Common.Utilities;
using Rihal.ReelRise.Domain.Constants;
using Rihal.ReelRise.Domain.Entities;
using Rihal.ReelRise.Domain.Enums;
using Rihal.ReelRise.Infrastructure.Identity;

namespace Rihal.ReelRise.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            string err = ex.Message + " ----->> " + _context.Database.GetConnectionString() + " ---- ";
            throw new Exception(err);
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                if (!_context.FilmCrews.Any())
                {
                    MovieDataSeeder movieDataSeeder = new MovieDataSeeder(_webHostEnvironment);

                    List<Movie>? movies = await movieDataSeeder.GetMovies();

                    if (movies is not null && movies.Count > 0)
                    {
                        await _context.Movies.AddRangeAsync(movies);
                        await _context.SaveChangesAsync();

                        var client = new HttpClient();

                        foreach (var movie in movies)
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, $"https://cinema.stag.rihal.tech/api/movie/{movie.Id}");
                            var response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();
                            var filmDetail = await response.Content.ReadAsStringAsync();

                            MovieDetailDto movieDetail = JsonConvert.DeserializeObject<MovieDetailDto>(filmDetail) ?? throw new InvalidOperationException("The filmDetail is null.");

                            movie.ReleaseDate = movieDetail.ReleaseDate.ToUtc();
                            movie.Budget = movieDetail.Budget;

                            if (movieDetail.MainCasts is not null && movieDetail.MainCasts.Count > 0)
                            {
                                foreach (var mainCastName in movieDetail.MainCasts)
                                {
                                    var cast = await GetFullCast(mainCastName);

                                    if (cast != null && cast.Id > 0)
                                    {
                                        movie.MovieFilmCrews.Add(new MovieFilmCrew
                                        {
                                            FilmCrewId = cast.Id,
                                            MovieId = movie.Id,
                                            CrewType = FilmCrewType.MainCast
                                        });
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(movieDetail.DirectorName))
                                {
                                    var director = await GetFullCast(movieDetail.DirectorName);

                                    movie.MovieFilmCrews.Add(new MovieFilmCrew
                                    {
                                        FilmCrewId = director.Id,
                                        MovieId = movie.Id,
                                        CrewType = FilmCrewType.Director
                                    });
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }

    private async Task<FilmCrew> GetFullCast(string mainCastName)
    {
        var filmCrews = await _context.FilmCrews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == mainCastName);

        if (filmCrews is not null && filmCrews.Id > 0)
        {
            return filmCrews;
        }
        else
        {
            FilmCrew filmCrew = new FilmCrew().Create(mainCastName);
            await _context.FilmCrews.AddAsync(filmCrew);
            await _context.SaveChangesAsync();

            return filmCrew;
        }
    }



    public class MovieDetailDto
    {
        [JsonProperty("release_date")]
        public string? release_date { get; set; }

        public DateTime? ReleaseDate
        {
            get
            {
                string[] splittedDate = release_date?.Split("-") ?? throw new InvalidDataException("release_date is null or empty");

                if (splittedDate.Length == 3)
                {
                    int year = Convert.ToInt32(splittedDate[2]);
                    int month = Convert.ToInt32(splittedDate[1]);
                    int day = Convert.ToInt32(splittedDate[0]);

                    return new DateTime(year, month, day, 0, 0, 0);
                }
                else
                {
                    return null;
                }
            }
        }

        [JsonProperty("main_cast")]
        public List<string>? MainCasts { get; set; }

        [JsonProperty("director")]
        public string? DirectorName { get; set; }

        public decimal Budget { get; set; }
    }
}

public class MovieDataSeeder(IWebHostEnvironment webHostEnvironment)
{

    public async Task<List<Movie>?> GetMovies()
    {
        string dirpath = webHostEnvironment.WebRootPath;

        string relativePath = @"InitialData/movies.json";
        string filePath = Path.GetFullPath(Path.Combine(dirpath, relativePath));

        string json = await File.ReadAllTextAsync(filePath);

        List<Movie>? movies = JsonConvert.DeserializeObject<List<Movie>>(json);
        return movies;
    }
}

