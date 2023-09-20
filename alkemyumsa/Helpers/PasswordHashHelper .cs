using System.Security.Cryptography;
using System.Text;

namespace alkemyumsa.Helpers
{
    public static class PasswordHashHelper
    {

        public static string EncryptPassword(string password, string email)
        {
            var salt = CreateSalt(email);
            string saltAndPwd = String.Concat(password, salt);
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var stream = Array.Empty<byte>();
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(saltAndPwd));
            for(int i = 0; i < stream.Length; i++) 
            {

                sb.AppendFormat("{0:x2}", stream[i]);

            }
            return sb.ToString();
        }

        private static string CreateSalt(string email)
        {
            var salt = email;
            byte[] saltBytes;
            string saltstr;
            saltBytes = ASCIIEncoding.UTF8.GetBytes(salt);
            long XORED = 0x00;

            foreach(byte x in saltBytes) 
            {
                XORED = XORED ^ x;   
            }

            Random rand = new Random(Convert.ToInt32(XORED));
            saltstr = rand.Next().ToString();
            saltstr += rand.Next().ToString();
            saltstr += rand.Next().ToString();
            saltstr += rand.Next().ToString();

            return salt;
        }
    }
}

