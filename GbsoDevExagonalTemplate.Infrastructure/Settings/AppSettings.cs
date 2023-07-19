namespace GbsoDevExagonalTemplate.Infrastructure.Settings
{
	public class AppSettings
	{
		public Dictionary<string, string> ConnectionStrings { get; set; }
		public AuthOptions AuthOptions { get; set; }
		public CorsOptions[] AllowCors { get; set; } = new CorsOptions[0];

		public string? GetConnectionString(string connection)
		{
			return ConnectionStrings[connection];
		}
	}
}
