using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Application.ValidationRules
{
	public static class EntityValidationRulesProvider
	{
		public static IServiceCollection AddDomainEntityValidationRules(this IServiceCollection services)
		{
			return services;
		}
	}
}
