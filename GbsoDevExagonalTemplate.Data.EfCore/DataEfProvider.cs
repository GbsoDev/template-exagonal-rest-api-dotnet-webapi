using GbsoDevExagonalTemplate.Data.EfCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	public static class DataEfProvider
	{
		public static IServiceCollection AddDataEfCoreRepositories(this IServiceCollection services)
		{
			return services;
		}
	}
}
