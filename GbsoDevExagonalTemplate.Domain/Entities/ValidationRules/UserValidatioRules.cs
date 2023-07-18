using FluentValidation;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Entities.ValidationRules;

namespace GbsoDevExagonalTemplate.Domain.EntityValidationRules
{
	public class UserValidatioRules : AbstractValidator<User>
	{
		public static readonly string VALID = "VALID";
		public UserValidatioRules()
		{
			RuleSet(ValidationRuleSets.CREATE, () =>
			{
			});
			RuleSet(ValidationRuleSets.UPDATE, () =>
			{
				RuleFor(n => n.Id)
					.NotEmpty();
			});
			RuleSet(ValidationRuleSets.DELETE, () =>
			{
				RuleFor(n => n.Id)
					.NotEmpty();
			});
			RuleSet(ValidationRuleSets.ALL, () =>
			{
				RuleFor(n => n.Name)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.Name)))
				.Length(5, 50).WithMessage(x => string.Format(ValidationRulesResx.NameLength, 5, 50));

				RuleFor(n => n.UserName)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.UserName)))
				.Length(10, 15).WithMessage(x => string.Format(ValidationRulesResx.UserNameLength, 10, 15));

				RuleFor(n => n.Password)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.Password)))
				.Length(10, 20).WithMessage(x => string.Format(ValidationRulesResx.PasswordLength, 10, 20));
			});
			RuleSet(VALID, () =>
			{
				RuleFor(n => n.UserName)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.UserName)));

				RuleFor(n => n.Password)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.Password)));
			});
		}
	}
}
