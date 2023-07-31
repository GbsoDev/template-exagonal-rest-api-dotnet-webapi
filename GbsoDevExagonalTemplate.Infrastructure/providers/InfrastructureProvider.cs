using GbsoDevExagonalTemplate.Application.ValidationRules;
using GbsoDevExagonalTemplate.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	public static class InfrastructureProvider
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, out AppSettings outAppSettings)
		{
			// WebApi settings
			services.AddWebApiSettings(configuration, out AppSettings appSettings);
			outAppSettings = appSettings;
			services.AddDomainEntityValidationRules()
				// Add application input casses
				.AddApplicationServices()
				// Add data repositories
				.AddDataEfCoreRepositories()
				// Add dbContext
				.AddDataEfCoreDbContext(appSettings)
				// WebApi token authentication
				.AddWebApiTokenAuthentication(appSettings)
				// WebApi dto mappers
				.AddMapperConfigurations()
				.AddWebApiCorsPolicies(appSettings);
			return services;
		}

		public static IApplicationBuilder WebApiPosBuild(this IApplicationBuilder app, IServiceProvider serviceProvider, AppSettings appSettings)
		{
			serviceProvider.MigrateDatabase();
			app.AddPosBuildWebApiCorsPolicies(appSettings);
			return app;
		}

		internal static IServiceCollection AddWebApiSettings(this IServiceCollection services, IConfiguration configuration, out AppSettings appSettings)
		{
			appSettings = new AppSettings();
			var builder = new ConfigurationBuilder();
			var combinedConfiguration = builder.AddConfiguration(configuration)
				.AddJsonFile(Constants.FILE_CONFIGURATION_PATH, false, true)
#if (DEBUG)
				.AddJsonFile(Constants.DEVELOPMENT_FILE_CONFIGURATION_PATH, true, true)
#endif
				.AddEnvironmentVariables()
				.Build();
			combinedConfiguration.Bind(appSettings);
			services.AddSingleton(appSettings);
			return services;
		}
	}
}
