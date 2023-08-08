using GbsoDevExagonalTemplate.Application;
using GbsoDevExagonalTemplate.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class ApplicationProvider
	{
		internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>()
				.AddScoped(serviceProvider => new Lazy<IUserService>(() => serviceProvider.GetRequiredService<IUserService>()));
			return services;
		}
	}
}
