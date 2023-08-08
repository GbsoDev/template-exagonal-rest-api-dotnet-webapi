using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Data.EfCore.MMSQL
{
	public static class DataEfMssqlProvider
	{
		public static IServiceCollection AddDataEfCoreMssqlDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<MSSQLDbContext>(options => options.UseSqlServer(connectionString));
			services.AddScoped<IMainContext, MSSQLDbContext>()
				.AddScoped(serviceProvider => new Lazy<IMainContext>(() => serviceProvider.GetRequiredService<IMainContext>()));
			return services;
		}
	}
}
