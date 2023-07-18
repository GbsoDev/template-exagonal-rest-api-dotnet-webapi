using FluentValidation;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.EntityValidationRules;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs;
using GbsoDevExagonalTemplate.Domain.Utils;

namespace GbsoDevExagonalTemplate.Application
{
	public class UserUseCase : EntityUseCase<User, int, IUserOutputPort>, IUserInputPort
	{
		public UserUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public override User Create(User user)
		{
			var validate = MainVr.Validate(user, x => x.IncludeRuleSets(ValidationRuleSets.CREATE, ValidationRuleSets.ALL));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			user.Password = PasswordHasher.GetSHA256(password: user.Password ?? string.Empty);
			var userRegistered = MainOutputPort.Register(user);
			MainOutputPort.SaveChanges();
			return userRegistered;
		}

		public bool ValidateUser(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user), "El usuario no puede ser null");
			var validate = MainVr.Validate(user, x => x.IncludeRuleSets(UserValidatioRules.VALID));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var encript = PasswordHasher.GetSHA256(password: user.Password ?? string.Empty);
			return MainOutputPort.ValidateUser(user.UserName ?? string.Empty, encript ?? string.Empty);
		}

		public void Enable(int id)
		{
			if (id <= 0) throw new ArgumentNullException(nameof(id), "El id no puede estár vacío");
			MainOutputPort.Enable(id);
			MainOutputPort.SaveChanges();
		}

		public void Desable(int id)
		{
			if (id <= 0) throw new ArgumentNullException(nameof(id), "El id no puede estár vacío");
			MainOutputPort.Disable(id);
			MainOutputPort.SaveChanges();
		}
	}
}
