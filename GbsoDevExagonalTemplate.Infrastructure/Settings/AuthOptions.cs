namespace GbsoDevExagonalTemplate.Infrastructure.Settings
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SigningKey { get; set; }
		public string[] Roles { get; set; }
	}
}
