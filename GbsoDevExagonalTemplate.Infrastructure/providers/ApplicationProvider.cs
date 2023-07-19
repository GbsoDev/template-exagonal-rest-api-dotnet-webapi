using GbsoDevExagonalTemplate.Application;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class ApplicationProvider
	{
		internal static IServiceCollection AddInputApplicationUseCases(this IServiceCollection services)
		{
			return services
				.AddScoped<IUserInputPort, UserUseCase>()
				.AddScoped(serviceProvider => new Lazy<IUserInputPort>(() => serviceProvider.GetRequiredService<IUserInputPort>()));
		}
	}
}
