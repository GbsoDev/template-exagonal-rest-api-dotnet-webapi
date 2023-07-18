using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs
{
	public interface IEntityInputPort<TEntity, TId> :
		ICreateInput<TEntity, TId>,
		IGetInput<TEntity, TId>,
		IUpdateInput<TEntity, TId>,
		IDeleteInput<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
	}
}
