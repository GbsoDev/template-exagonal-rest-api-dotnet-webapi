using GbsoDevExagonalTemplate.Application;
using GbsoDevExagonalTemplate.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class ApplicationProvider
	{
		internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services;
		}
	}
}
