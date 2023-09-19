using System.Security.Cryptography;
using System.Text;

namespace alkemyumsa.Helpers
{
    public static class PasswordHashHelper
    {

        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var stream = Array.Empty<byte>();
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for(int i = 0; i < stream.Length; i++) {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}

