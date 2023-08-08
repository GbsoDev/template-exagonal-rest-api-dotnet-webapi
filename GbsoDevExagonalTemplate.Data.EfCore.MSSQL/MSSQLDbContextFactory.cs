using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GbsoDevExagonalTemplate.Data.EfCore.MMSQL
{
	public class MSSQLDbContextFactory : IDesignTimeDbContextFactory<MSSQLDbContext>
	{
		public MSSQLDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<MSSQLDbContext>();
			var rooConnectionString = BuildConfiguration().GetConnectionString(CONNECTION_DB_NAME);
			optionsBuilder.UseSqlServer(rooConnectionString);
			return new MSSQLDbContext(optionsBuilder.Options);
		}

		public const string CONNECTION_DB_NAME = "migrations-connection-db";
		public const string FILE_CONFIGURATION_PATH = "appsettings.json";

		private IConfigurationRoot BuildConfiguration()
		{
			var configurationResult = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(FILE_CONFIGURATION_PATH, optional: false, reloadOnChange: true)
				.Build();
			return configurationResult;
		}
	}
}

