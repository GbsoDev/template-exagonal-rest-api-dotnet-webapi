using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;

namespace GbsoDevExagonalTemplate.Application.Interfaces.Actions
{
	public interface ICreateAction<IEntity, TId>
		where IEntity : class, IEntity<TId>
	{
		Task<IEntity> CreateAsync(IEntity entity, CancellationToken cancellationToken = default);
	}
}
