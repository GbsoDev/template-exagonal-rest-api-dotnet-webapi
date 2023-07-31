using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces.Actions
{
	public interface IUpdateAction<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}
