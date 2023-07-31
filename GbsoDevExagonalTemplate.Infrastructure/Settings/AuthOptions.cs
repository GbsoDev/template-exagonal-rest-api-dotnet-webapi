namespace GbsoDevExagonalTemplate.Infrastructure.Settings
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SigningKey { get; set; }
		public string[] Roles { get; set; }

		public AuthOptions()
		{
			Issuer = string.Empty;
			Audience = string.Empty;
			SigningKey = string.Empty;
			Roles = new string[0];
		}
	}
}
