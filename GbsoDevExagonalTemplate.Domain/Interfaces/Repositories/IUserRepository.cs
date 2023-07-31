using GbsoDevExagonalTemplate.Domain.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IEntityRepository<User, int>
	{
		Task<bool> ValidateUserAsync(string userName, string encript, CancellationToken cancellationToken = default);
		Task EnableAsync(int id, CancellationToken cancellationToken = default);
		Task DisableAsync(int id, CancellationToken cancellationToken = default);
	}
}
