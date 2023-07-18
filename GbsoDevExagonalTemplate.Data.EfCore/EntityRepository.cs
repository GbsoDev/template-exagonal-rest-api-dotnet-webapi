using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Interfaces.Entities;
using GbsoDevExagonalTemplate.Domain.Interfaces.Ports.Outputs;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public abstract class EntityRepository<TEntity, TId> : IEntityOutputPort<TEntity, TId>
		where TEntity : class, IEntity<TId>
	{
		protected IMainContext MainContext { get; }
		private Lazy<IMainContext> mainContext;

		public EntityRepository(IServiceProvider serviceProvider)
		{
			MainContext = ActivatorUtilities.GetServiceOrCreateInstance<IMainContext>(serviceProvider);
		}

		public virtual TEntity Register(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			MainContext.Set<TEntity>().Add(entity);
			return entity;
		}

		public virtual TEntity? GetById(TId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var result = MainContext.Find<TEntity>(id);
			return result;
		}

		public virtual TEntity[] List()
		{
			var result = MainContext.Set<TEntity>().ToArray();
			return result;
		}

		public virtual TEntity Update(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = MainContext.Find<TEntity>(entity.Id);
			if (entityResult == null) throw new ApplicationException("Está intentando actualizar un registro que no existe");
			if (typeof(TEntity).GetInterface(nameof(IAuditableEntity<TId>)) != null)
				((IAuditableEntity<TId>)entity).CreatedDate = ((IAuditableEntity<TId>)entityResult).CreatedDate;
			MainContext.Entry(entityResult).CurrentValues.SetValues(entity);
			return entityResult;
		}

		public virtual TEntity? Update(TEntity entity, Expression<Func<TEntity, object>> @object)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = MainContext.Find<TEntity>(entity.Id);
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

		public void SaveChanges()
		{
			MainContext.SaveChanges();
		}
	}
}
