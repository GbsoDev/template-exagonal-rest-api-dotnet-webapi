using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GbsoDevExagonalTemplate.Application
{
	public abstract class UseCase
	{
		public ILogger<UseCase> Logger { get; set; }

		public UseCase(IServiceProvider serviceProvider)
		{
			Logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<UseCase>>(serviceProvider);
		}
	}
}
