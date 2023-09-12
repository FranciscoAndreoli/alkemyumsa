using alkemyumsa.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace alkemyumsa.Helpers
{
    public class TokenJwtHelper // utilizamos esta clase para generar el token.
    {
        private IConfiguration _configuration; // Nos permite acceder a la configuración de la aplicación, almacenada en appsettings.json

        public TokenJwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuarios user)
        {
            var claims = new[] // afirmaciones del token.
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]), // sujeto del token, traído de appsetting.json
                new Claim(ClaimTypes.Email, user.Email), // email del objeto user.
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // identificador unico del usuario, usamos el id del objeto user.
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); // Se crea una clave simétrica a partir del key presente en appsetting.json
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // se crea la credencial, usamos la key previa y el algoritmo de encriptación

            var securityToken = new JwtSecurityToken( //Se crea el token JWT
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
