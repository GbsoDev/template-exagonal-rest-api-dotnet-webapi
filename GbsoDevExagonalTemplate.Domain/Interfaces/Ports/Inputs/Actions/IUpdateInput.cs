using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions
{
	public interface IUpdateInput<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		TEntity Update(TEntity entity);
	}
}
