using GbsoDevExagonalTemplate.Data.EfCore;
using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class DataEfProvider
	{
		internal static IServiceCollection AddDataEfCoreDbContext(this IServiceCollection services, string connectionString)
		{
			return services.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString))
				.AddScoped<IMainContext, MainContext>()
				.AddScoped(serviceProvider => new Lazy<IMainContext>(() => serviceProvider.GetRequiredService<IMainContext>()));
		}

		internal static IServiceCollection AddOutputDataEfCoreRepositories(this IServiceCollection services)
		{
			return services
				.AddScoped<IUserOutputPort, UserRepository>()
				.AddScoped(serviceProvider => new Lazy<IUserOutputPort>(() => serviceProvider.GetRequiredService<IUserOutputPort>()));
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
					Console.WriteLine($"Error al ejecutar la migración: {Environment.NewLine}{ex}");
				}
			}
			return service;
		}
	}
}
