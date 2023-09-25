using alkemyumsa.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace alkemyumsa.Helpers
{
    /// <summary>
    /// Clase auxiliar para la generación de tokens JWT.
    /// </summary>
    public class TokenJwtHelper
    {
        private IConfiguration _configuration;

        /// <summary>
        /// Constructor que inicializa la configuración para la generación del token.
        /// </summary>
        /// <param name="configuration">Configuración utilizada para la creación del token JWT.</param>
        public TokenJwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Genera un token JWT basado en la información del usuario proporcionado.
        /// </summary>
        /// <param name="user">El usuario para el cual se generará el token.</param>
        /// <returns>Retorna el token JWT generado como una cadena de texto.</returns>
        public string GenerateToken(Usuarios user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
