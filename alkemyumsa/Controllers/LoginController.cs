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
using alkemyumsa.Infraestructure;

namespace alkemyumsa.Controllers
{

    /// <summary>
    /// Controlador responsable de las operaciones del login.
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase 
    {
        private TokenJwtHelper _tokenJwtHelper;
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LoginController"/>.
        /// </summary>
        /// <param name="unitOfWork">Provee mecanismos para interactuar con la base.</param>
        /// <param name="configuration">Representa un conjunto de configuraciones de la aplicación.</param>
        public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        /// <summary>
        /// Autentica el usuario.
        /// </summary>
        /// <param name="loginDto">El DTO que contiene información para la autenticación.</param>
        /// <returns>
        /// Devuelve el usuario con un token generado. 
        /// Si falla, devuelve un error 401.
        /// </returns>
        [HttpPost]
        //[AllowAnonymous] 
        public async Task<IActionResult> Login([FromBody]AuthenticateDto loginDto)
        {
            try
            {
                var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(loginDto);
                if (userCredentials == null) { return ResponseFactory.CreateErrorResponse(401, "Credenciales incorrectas!"); } 

                var token = _tokenJwtHelper.GenerateToken(userCredentials);
                var user = new UsuarioLoginDto()
                {
                    Nombre = userCredentials.Nombre,
                    Dni = userCredentials.Dni,
                    Email = userCredentials.Email,
                    Token = token

                };
                return ResponseFactory.CreateSuccessResponse(200, user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(401, $"Error del sistema: {ex.Message}");
            }
        }
    }
}




