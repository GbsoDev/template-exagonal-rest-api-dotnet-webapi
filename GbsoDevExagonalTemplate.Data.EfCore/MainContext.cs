using GbsoDevExagonalTemplate.Data.EfCore.Configurations;
using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public class MainContext<TContext> : DbContext, IMainContext where TContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public MainContext(DbContextOptions<TContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UserConfiguration());
		}
	}
}
