using GbsoDevExagonalTemplate.Data.EfCore;
using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using GbsoDevExagonalTemplate.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class DataEfProvider
	{
		internal static IServiceCollection AddDataEfCoreDbContext(this IServiceCollection services, AppSettings appSettings)
		{
#if DEBUG
			var connectionName = appSettings.Environments.RunningInDocker ? Constants.CONNECTION_DB_NAME : Constants.CONNECTION_DB_NAME_LOCAL;
#else
			var connectionName = Constants.CONNECTION_DB_NAME;
#endif
			var connectionString = appSettings.GetConnectionString(connectionName) ?? throw new ApplicationException("Conexión a base de datos no encontrada");
			
			services.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString));
			return services;
		}

		internal static IServiceCollection AddDataEfCoreRepositories(this IServiceCollection services)
		{
			return services;
		}

		internal static IServiceProvider MigrateDatabase(this IServiceProvider service)
		{
			using (var scope = service.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var dbContext = services.GetRequiredService<MainContext>();
					dbContext.Database.Migrate();
				}
				catch (Exception ex)
				{
					throw new ApplicationException("Error al ejecutar la migración", ex);
				}
			}
			return service;
		}
	}
}
