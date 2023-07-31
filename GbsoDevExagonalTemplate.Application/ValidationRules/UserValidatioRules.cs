using FluentValidation;
using GbsoDevExagonalTemplate.Domain.Entities;

namespace GbsoDevExagonalTemplate.Application.ValidationRules
{
	public class UserValidatioRules : AbstractValidator<User>
	{
		public static readonly string VALID = "VALID";
		public UserValidatioRules()
		{
			RuleSet(ValidationRuleSets.ID, () =>
			{
				RuleFor(n => n.Id)
				.NotEmpty().WithMessage(x => string.Format(ValidationRulesResx.PropertyEmpty, nameof(x.Name)));
			});

			RuleSet(ValidationRuleSets.TO_CREATE, () =>
			{
				RuleFor(x => x).SetValidator(this, ruleSets: ValidationRuleSets.ID);

				RuleFor(x => x).SetValidator(this, ruleSets: ValidationRuleSets.ALL_EXCEPT_ID);
			});
			RuleSet(ValidationRuleSets.TO_UPDATE, () =>
			{
				RuleFor(x => x).SetValidator(this, ruleSets: ValidationRuleSets.ID);

				RuleFor(x => x).SetValidator(this, ruleSets: ValidationRuleSets.ALL_EXCEPT_ID);
			});
			RuleSet(ValidationRuleSets.TO_DELETE, () =>
			{
				RuleFor(x => x).SetValidator(this, ruleSets: ValidationRuleSets.ID);
			});
			RuleSet(ValidationRuleSets.ALL_EXCEPT_ID, () =>
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
