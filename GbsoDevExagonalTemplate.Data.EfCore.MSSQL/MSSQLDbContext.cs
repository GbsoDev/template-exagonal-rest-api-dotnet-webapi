using Microsoft.EntityFrameworkCore;

namespace GbsoDevExagonalTemplate.Data.EfCore.MMSQL
{
	public sealed class MSSQLDbContext : MainContext<MSSQLDbContext>
	{
		public MSSQLDbContext(DbContextOptions<MSSQLDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
