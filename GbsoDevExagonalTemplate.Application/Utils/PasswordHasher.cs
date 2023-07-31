using System.Security.Cryptography;
using System.Text;

namespace GbsoDevExagonalTemplate.Application.Utils
{
	public static class PasswordHasher
	{
		public static string GetSHA256(string password)
		{
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "El texto no puede estar vacío");
			SHA256 sha256 = SHA256.Create();
			byte[] textOriginal = Encoding.Default.GetBytes(password);
			byte[] hash = sha256.ComputeHash(textOriginal);
			StringBuilder cadena = new StringBuilder();
			foreach (byte i in hash)
			{
				cadena.AppendFormat("{0:x2}", i);
			}
			return cadena.ToString();
		}
	}
}
