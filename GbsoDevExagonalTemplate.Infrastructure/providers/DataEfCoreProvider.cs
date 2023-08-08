using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Data.EfCore.MMSQL;
using GbsoDevExagonalTemplate.Data.EfCore.InMemory;
using GbsoDevExagonalTemplate.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class DataEfCoreProvider
	{
		internal static IServiceCollection AddDataEfCoreDbContext(this IServiceCollection services, AppSettings appSettings)
		{
			switch (appSettings.DbType)
			{
				case DbType.MSSQL:
					{

#if DEBUG
						var connectionName = appSettings.Environments.RunningInDocker ? Constants.CONNECTION_DB_NAME : Constants.CONNECTION_DB_NAME_LOCAL;
#else
						var connectionName = Constants.CONNECTION_DB_NAME;
#endif
						var connectionString = appSettings.GetConnectionString(connectionName) ?? throw new ApplicationException("Conexión a base de datos no encontrada");
						services.AddDataEfCoreMssqlDbContext(connectionString);
						break;
					}
				case DbType.InMemory:
					{
						var dbName = appSettings.GetConnectionString(Constants.IN_MEMORY_DB_NAME) ?? throw new ApplicationException("Conexión a base de datos no encontrada");
						services.AddDataEfCoreInMemoryDbContext(dbName);
						break;
					}
				default:
					throw new ApplicationException("Error en configuración de base de datos");
			}
			return services;

		}

		internal static IServiceProvider MigrateDatabase(this IServiceProvider service, AppSettings appSettings)
		{
			switch (appSettings.DbType)
			{
				case DbType.MSSQL:
				case DbType.PostgreSql:
					using (var scope = service.CreateScope())
					{
						var services = scope.ServiceProvider;
						try
						{
							var dbContext = services.GetRequiredService<IMainContext>();
							dbContext.Database.Migrate();
						}
						catch (Exception ex)
						{
							throw new ApplicationException("Error al ejecutar la migración", ex);
						}
					}
					break;
				default:
					break;
			}
			
			return service;
		}
	}
}
