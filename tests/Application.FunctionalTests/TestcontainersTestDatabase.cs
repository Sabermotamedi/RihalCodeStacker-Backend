using System.Data.Common;
using Rihal.ReelRise.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Respawn;
using Npgsql;
using Testcontainers.PostgreSql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Rihal.ReelRise.Application.FunctionalTests;

public class TestcontainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public TestcontainersTestDatabase()
    {

        _container = new PostgreSqlBuilder().WithDatabase("ReelRise").WithUsername("postgres").WithPassword("123").Build();

    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        await context.Database.MigrateAsync();

        var respawnerOptions = new RespawnerOptions
        {
            SchemasToInclude = new[] { "public" },
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        };

        await _connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);

       

    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
