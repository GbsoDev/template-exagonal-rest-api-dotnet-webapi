using GbsoDevExagonalTemplate.Data.EfCore;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	public static class DataEfProvider
	{
		public static IServiceCollection AddDataEfCoreRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>()
				.AddScoped(serviceProvider => new Lazy<IUserRepository>(() => serviceProvider.GetRequiredService<IUserRepository>()));
			return services;
		}
	}
}
