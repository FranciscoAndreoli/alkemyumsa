using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using alkemyumsa.Helpers;
using alkemyumsa.Services;
using Microsoft.AspNetCore.Authorization;

namespace alkemyumsa.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class LoginController : ControllerBase //LoginController es el encargado de devolver el token, también será utilizado en el front. y en postman.
    {
        private TokenJwtHelper _tokenJwtHelper;
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        /// <summary>
        /// Borra usuario.
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns>Retorna not found si no se encontró usuario. Si se encontró, no retorna contenido</returns>
        [HttpPost]
        [AllowAnonymous] // permite ingresar al endpoint, ignorando el '[Authorize]' de arriba.
        public async Task<IActionResult> Login([FromBody]AuthenticateDto loginDto)
        {
            try
            {
                var userCredentials = await _unitOfWork.UserRepository.authenticateCredentials(loginDto);
                if (userCredentials == null) { return Unauthorized("Credencial incorrecta!"); } 

                var token = _tokenJwtHelper.GenerateToken(userCredentials);
                var user = new UsuarioLoginDto()
                {
                    Nombre = userCredentials.Nombre,
                    Apellido = userCredentials.Apellido,
                    Email = userCredentials.Email,
                    Token = token

                };
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error del sistema: {ex.Message}");
            }
        }
    }
}




