using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions
{
	public interface IGetInput<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		TEntity[] Get();

		TEntity? GetById(TId id);
	}
}
