using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions
{
	public interface IDeleteInput<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		void Delete(TEntity entity);
	}
}
