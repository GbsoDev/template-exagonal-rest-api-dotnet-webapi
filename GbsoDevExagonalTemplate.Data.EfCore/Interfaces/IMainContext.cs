using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GbsoDevExagonalTemplate.Data.EfCore.Interfaces
{
	public interface IMainContext
	{
		DbSet<User> Users { get; set; }

		int SaveChanges(bool acceptAllChangesOnSuccess);

		TEntity? Find<TEntity>(params object?[]? keyValues) where TEntity : class;

		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		EntityEntry Entry(object entity);

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

		void RemoveRange(IEnumerable<object> entities);
		int SaveChanges();
	}
}
