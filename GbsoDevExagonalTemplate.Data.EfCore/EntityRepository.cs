using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public abstract class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		protected IMainContext MainContext { get; }

		public EntityRepository(IServiceProvider serviceProvider)
		{
			MainContext = ActivatorUtilities.GetServiceOrCreateInstance<IMainContext>(serviceProvider);
		}

		public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			await MainContext.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		public virtual Task<TEntity[]> ListAsync(CancellationToken cancellationToken = default)
		{
			var result = MainContext.Set<TEntity>().ToArrayAsync(cancellationToken);
			return result;
		}

		public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var keyValues = new object[] { id };
			var result = await MainContext.FindAsync<TEntity>(keyValues, cancellationToken);
			return result;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await this.GetByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			if (typeof(TEntity).GetInterface(nameof(IAuditableEntity<TId>)) != null)
			{
				((IAuditableEntity<TId>)entity).CreatedDate = ((IAuditableEntity<TId>)entityResult).CreatedDate;
				((IAuditableEntity<TId>)entity).UpdatedDate = DateTime.Now;
			}
			MainContext.Entry(entityResult).CurrentValues.SetValues(entity);
			return entityResult;
		}

		public virtual async Task<TEntity?> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> @object, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await GetByIdAsync(entity.Id, cancellationToken);
			var objectResult = @object?.Compile()?.Invoke(entity);
			if (entityResult != null && objectResult != null)
				MainContext.Entry(entity).CurrentValues.SetValues(objectResult);
			return entityResult;
		}

		public virtual void Delete(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			MainContext.Remove(entity);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await MainContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
