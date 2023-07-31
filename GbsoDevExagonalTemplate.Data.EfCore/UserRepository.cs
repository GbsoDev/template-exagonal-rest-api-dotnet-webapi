using GbsoDevExagonalTemplate.Domain.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public sealed class UserRepository : EntityRepository<User, int>, IUserRepository
	{

		public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public async Task DisableAsync(int id, CancellationToken cancellationToken = default)
		{
			await ChageStateAsync(id, false, cancellationToken);
		}

		public async Task EnableAsync(int id, CancellationToken cancellationToken = default)
		{
			await ChageStateAsync(id, true, cancellationToken);
		}

		private async Task ChageStateAsync(int id, bool state, CancellationToken cancellationToken = default)
		{
			if (id < 1) throw new ArgumentNullException(nameof(id), "El id no puede ser 0");
			var entryUser = await GetByIdAsync(id, cancellationToken);
			if (entryUser == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			entryUser.Enabled = state;
		}

		public async Task<bool> ValidateUserAsync(string userName, string encript, CancellationToken cancellationToken = default)
		{
			return await MainContext.Users.AnyAsync(x => x.UserName == userName && x.Enabled == true && x.Password == encript, cancellationToken);
		}
	}
}
