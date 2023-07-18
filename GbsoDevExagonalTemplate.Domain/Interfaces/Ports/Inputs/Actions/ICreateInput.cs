using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Inputs.Actions
{
	public interface ICreateInput<IEntity, TId>
		where IEntity : class, IEntity<TId>
	{
		IEntity Create(IEntity entity);
	}
}
