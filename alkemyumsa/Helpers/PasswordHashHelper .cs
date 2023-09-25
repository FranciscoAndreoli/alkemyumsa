using System.Security.Cryptography;
using System.Text;

namespace alkemyumsa.Helpers
{
    /// <summary>
    /// Clase auxiliar para la creación de contraseñas encriptadas y gestión de sal (salt).
    /// </summary>
    public static class PasswordHashHelper
    {
        /// <summary>
        /// Encripta una contraseña utilizando SHA256 con un salt basado en el email del usuario.
        /// </summary>
        /// <param name="password">Contraseña del usuario a encriptar.</param>
        /// <param name="email">Email del usuario que se utiliza para crear el salt.</param>
        /// <returns>Retorna la contraseña encriptada en formato de cadena hexadecimal.</returns>
        public static string EncryptPassword(string password, string email)
        {
            var salt = CreateSalt(email);
            string saltAndPwd = String.Concat(password, salt);
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var stream = Array.Empty<byte>();
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(saltAndPwd));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Crea un salt basado en el email proporcionado.
        /// </summary>
        /// <param name="email">Email del usuario que se utilizará como base para crear el salt.</param>
        /// <returns>Retorna una cadena que representa el salt creado a partir del email.</returns>
        private static string CreateSalt(string email)
        {
            var salt = email;
            byte[] saltBytes;
            string saltstr;
            saltBytes = ASCIIEncoding.UTF8.GetBytes(salt);
            long XORED = 0x00;

            foreach (byte x in saltBytes)
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
