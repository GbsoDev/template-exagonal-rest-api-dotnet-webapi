using GbsoDevExagonalTemplate.Data.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public class MainContext<TContext> : DbContext, IMainContext where TContext : DbContext
	{
		public MainContext(DbContextOptions<TContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
