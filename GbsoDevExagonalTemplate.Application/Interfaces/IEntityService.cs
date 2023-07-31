using GbsoDevExagonalTemplate.Application.Interfaces.Actions;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces
{
	public interface IEntityService<TEntity, TId> :
		ICreateAction<TEntity, TId>,
		IGetAction<TEntity, TId>,
		IUpdateAction<TEntity, TId>,
		IDeleteAction<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
	}
}
