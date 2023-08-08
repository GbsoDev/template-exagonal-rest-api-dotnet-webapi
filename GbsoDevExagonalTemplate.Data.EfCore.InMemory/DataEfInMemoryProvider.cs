using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Data.EfCore.InMemory
{
	public static class DataEfInMemoryProvider
	{
		public static IServiceCollection AddDataEfCoreInMemoryDbContext(this IServiceCollection services, string inMemoryDbName)
		{
			services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase(inMemoryDbName));
			services.AddScoped<IMainContext, InMemoryDbContext>()
				.AddScoped(serviceProvider => new Lazy<IMainContext>(() => serviceProvider.GetRequiredService<IMainContext>()));
			return services;
		}
	}
}
