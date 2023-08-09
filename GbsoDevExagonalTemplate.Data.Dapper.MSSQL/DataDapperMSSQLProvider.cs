using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Data.Dapper.MSSQL
{
	public static class DataDapperMSSQLProvider
	{
		public static IServiceCollection AddDataEfCoreRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>()
				.AddScoped(serviceProvider => new Lazy<IUserRepository>(() => serviceProvider.GetRequiredService<IUserRepository>()));
			return services;
		}
	}
}
