using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GbsoDevExagonalTemplate.Data.EfCore
{
	public class DbContextFactory : IDesignTimeDbContextFactory<MainContext>
	{
		/// <summary>
		/// Con la consola del administrador de paquetes, podremos ejecutar Add-Migration "Drop-Database ,Get-DbContext ,Scaffold-DbContext
		///Script-Migrations ,Update-Database", Recibe variables de entorno indicando en optionsbuilder Environment.GetEnvironmentVariable(""),
		///o solo pasar la cadena de conexion local estrutura ->"Server=localhost;Port=5432;Database=namedatabase;Usuario Id=user;Password=password;"
		/// Get-Help about_EntityFrameworkCore -> comando para visualizar las ayudas
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public MainContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
			var rooConnectionString = BuildConfiguration().GetConnectionString(CONNECTION_DB_NAME);
			optionsBuilder.UseSqlServer(rooConnectionString);
			return new MainContext(optionsBuilder.Options);
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

