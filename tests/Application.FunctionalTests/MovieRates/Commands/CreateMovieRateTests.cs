using Rihal.ReelRise.Application.Common.Exceptions;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;
using Rihal.ReelRise.Application.Movies.Queries;

namespace Rihal.ReelRise.Application.FunctionalTests.MovieRates.Commands;
using static Testing;

public class CreateMovieRateTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateMovieRateCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task Should_MovieRate_Must_Be_Greater_That_Or_Equal_To_One()
    {
        var command = new CreateMovieRateCommand() { MovieId = 1, Rate = 0 };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Rate")))
                .And.Errors["Rate"].Should().Contain("'Rate' must be greater than or equal to '1'.");
    }


    [Test]
    public async Task Should_MovieRate_Less_Than_Or_Equal_To_Ten()
    {
        var command = new CreateMovieRateCommand() { MovieId = 1, Rate = 11 };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Rate")))
                .And.Errors["Rate"].Should().Contain("'Rate' must be less than or equal to '10'.");
    }


    [Test]
    public async Task Should_Movie_Exist()
    {
        var command = new CreateMovieRateCommand() { MovieId = 0, Rate = 5 };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("MovieId")))
                .And.Errors["MovieId"].Should().Contain("This movie is not exist");
    }

    [Test]
    public async Task Should_Create_MovieRate()
    {
        var userId = await RunAsDefaultUserAsync();

        await SendAsync(new CreateMovieRateCommand
        {
            MovieId = 1,
            Rate = 5
        });

        var command = new GetMovieByIdQuery
        {
            Id = 1
        };

        var movie = await SendAsync(command);

        movie.Should().NotBeNull();
        movie.Id.Should().Be(1);
        movie.Name.Should().Be("Mad Max");
        movie.MyRate.Should().Be(5);
    }

    [Test]
    public async Task Should_Prevent_Duplicate_Vote()
    {
        var userId = await RunAsDefaultUserAsync();

        await SendAsync(new CreateMovieRateCommand
        {
            MovieId = 10,
            Rate = 5
        });

        var command = new CreateMovieRateCommand
        {
            MovieId = 10,
            Rate = 6
        };

        (await FluentActions.Invoking(() =>
        SendAsync(command))
              .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("MovieId")))
              .And.Errors["MovieId"].Should().Contain("You have already voted for this movie, you are not allowed to vote again");
    }
}
