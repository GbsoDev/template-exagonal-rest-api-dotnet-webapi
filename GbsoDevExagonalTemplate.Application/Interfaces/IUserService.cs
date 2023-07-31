using GbsoDevExagonalTemplate.Application.Interfaces.Actions;
using GbsoDevExagonalTemplate.Domain.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces
{
	public interface IUserService :
		ICreateAction<User, int>,
		IGetAction<User, int>,
		IUpdateAction<User, int>
	{
		Task<bool> ValidateUserAsync(User user, CancellationToken cancellationToken = default);
		Task EnableAsync(int id, CancellationToken cancellationToken = default);
		Task DesableAsync(int id, CancellationToken cancellationToken = default);
	}
}
