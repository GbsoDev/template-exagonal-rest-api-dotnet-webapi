using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDevExagonalTemplate.Api
{
	public abstract class BaseController : ControllerBase
	{
		protected readonly ILogger Logger;
		protected readonly IMapper Mapper;

		public BaseController(IServiceProvider serviceProvider)
		{
			Logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<BaseController>>(serviceProvider);
			Mapper = ActivatorUtilities.GetServiceOrCreateInstance<IMapper>(serviceProvider);
		}
	}
}