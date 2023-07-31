using FluentValidation;
using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDevExagonalTemplate.Application.ValidationRules
{
	public static class EntityValidationRulesProvider
	{
		public static IServiceCollection AddDomainEntityValidationRules(this IServiceCollection services)
		{
			services.AddSingleton<IValidator<User>, UserValidatioRules>();
			return services;
		}
	}
}
