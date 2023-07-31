using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces.Actions
{
	public interface IDeleteAction<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
		Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}
