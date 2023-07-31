namespace GbsoDevExagonalTemplate.Infrastructure.Settings
{
	public class AppSettings
	{
		public Dictionary<string, string> ConnectionStrings { get; set; }
		public AuthOptions AuthOptions { get; set; }
		public CorsOptions[] AllowCors { get; set; }
		public Environments Environments { get; set; }

		public AppSettings()
		{
			ConnectionStrings = new Dictionary<string, string>();
			AuthOptions = new AuthOptions();
			AllowCors = new CorsOptions[0];
			Environments = new Environments();
		}

		public string? GetConnectionString(string connection)
		{
			return ConnectionStrings[connection];
		}
	}
}
