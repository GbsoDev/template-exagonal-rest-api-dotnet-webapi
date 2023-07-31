using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GbsoDevExagonalTemplate.Application
{
	public abstract class BaseService
	{
		public ILogger<BaseService> Logger { get; set; }

		public BaseService(IServiceProvider serviceProvider)
		{
			Logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<BaseService>>(serviceProvider);
		}
	}
}
