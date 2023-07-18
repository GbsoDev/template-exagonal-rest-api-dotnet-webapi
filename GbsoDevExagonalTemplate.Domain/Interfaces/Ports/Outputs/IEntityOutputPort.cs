using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs
{
	public interface IEntityOutputPort<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		TEntity Register(TEntity entity);

		TEntity[] List();

		TEntity? GetById(TId id);

		TEntity Update(TEntity entity);

		void Delete(TEntity entity);

		void SaveChanges();
	}
}
