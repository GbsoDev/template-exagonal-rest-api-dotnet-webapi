using FluentValidation;
using GbsoDevExagonalTemplate.Application.Interfaces;
using GbsoDevExagonalTemplate.Application.Utils;
using GbsoDevExagonalTemplate.Application.ValidationRules;
using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;

namespace GbsoDevExagonalTemplate.Application
{
	public class UserService : EntityBaseService<User, int, IUserRepository>, IUserService
	{
		public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public override async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
		{
			var validate = MainValidationRules.Validate(user, x => x.IncludeRuleSets(ValidationRuleSets.TO_CREATE, ValidationRuleSets.ALL_EXCEPT_ID));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			user.Password = PasswordHasher.GetSHA256(password: user.Password ?? string.Empty);
			var userRegistered = await MainRepository.AddAsync(user, cancellationToken);
			await MainRepository.SaveChangesAsync(cancellationToken);
			return userRegistered;
		}

		public async Task<bool> ValidateUserAsync(User user, CancellationToken cancellationToken = default)
		{
			if (user == null) throw new ArgumentNullException(nameof(user), "El usuario no puede ser null");
			var validate = MainValidationRules.Validate(user, x => x.IncludeRuleSets(UserValidatioRules.VALID));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			var encript = PasswordHasher.GetSHA256(password: user.Password ?? string.Empty);
			return await MainRepository.ValidateUserAsync(user.UserName ?? string.Empty, encript ?? string.Empty, cancellationToken);
		}

		public async Task EnableAsync(int id, CancellationToken cancellationToken = default)
		{
			if (id <= 0) throw new ArgumentNullException(nameof(id), "El id no puede estár vacío");
			await MainRepository.EnableAsync(id, cancellationToken);
			await MainRepository.SaveChangesAsync(cancellationToken);
		}

		public async Task DesableAsync(int id, CancellationToken cancellationToken = default)
		{
			if (id <= 0) throw new ArgumentNullException(nameof(id), "El id no puede estár vacío");
			await MainRepository.DisableAsync(id, cancellationToken);
			await MainRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
