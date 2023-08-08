using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore.InMemory
{
	public sealed class InMemoryDbContext : MainContext<InMemoryDbContext>
	{
		public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
