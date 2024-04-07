using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Constants;
using Rihal.ReelRise.Infrastructure.Data;
using Rihal.ReelRise.Infrastructure.Data.Interceptors;
using Rihal.ReelRise.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Rihal.ReelRise.Infrastructure.Storage.GoogleStorage;
using Rihal.ReelRise.Infrastructure.Storage.AwsStorage;
using Rihal.ReelRise.Infrastructure.Service;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = string.Empty;

        if (IsRunningInDocker())
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME");

            //connectionString = $"Host={dbHost};Port=5432;Username={dbUsername};Password={dbPassword};Database={dbName};Pooling=true;";
            connectionString = $"Host=localhost;Port=5432;Username=postgres;Password=123;Database=ReelRise1;Pooling=true;";
        }
        else
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        services.AddScoped<IS3Storage, S3Storage>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ITextService, TextService>();

        return services;
    }

    private static bool IsRunningInDocker()
    {
        var isDockerCGroup = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_HOST"));
        var isDockerFilePresent = System.IO.File.Exists("/.dockerenv");

        return isDockerCGroup || isDockerFilePresent;
    }
}
