using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Data.Dapper.MSSQL
{
	public static class DataDapperMSSQLProvider
	{
		public static IServiceCollection AddDataEfCoreRepositories(this IServiceCollection services)
		{
			return services;
		}
	}
}
