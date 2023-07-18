using FluentValidation;
using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Domain.EntityValidationRules
{
	public static class EntityValidationRulesProvider
	{
		public static IServiceCollection AddDomainEntityValidationRules(this IServiceCollection services)
		{
			return services;
		}
	}
}
