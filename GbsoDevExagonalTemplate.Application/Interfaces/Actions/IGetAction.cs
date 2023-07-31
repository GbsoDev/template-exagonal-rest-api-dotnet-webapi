using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces.Actions
{
	public interface IGetAction<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		Task<TEntity[]> GetAsync(CancellationToken cancellationToken = default);

		Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
	}
}
