using GbsoDevExagonalTemplate.Data.EfCore.Configurations;
using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using GbsoDevExagonalTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public sealed class MainContext : DbContext, IMainContext
	{
		public DbSet<User> Users { get; set; }

		public MainContext(DbContextOptions<MainContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UserConfiguration());
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
