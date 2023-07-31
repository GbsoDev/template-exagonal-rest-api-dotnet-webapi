using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Repositories
{
	public interface IEntityRepository<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<TEntity[]> ListAsync(CancellationToken cancellationToken = default);

		Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task DeleteAsync(TEntity entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
