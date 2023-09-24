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
    /// Controller responsible for handling user login operations.
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase 
    {
        private TokenJwtHelper _tokenJwtHelper;
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="unitOfWork">Provides mechanisms to interact with the data source, such as database operations.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        /// <summary>
        /// Authenticates the user based on the provided credentials.
        /// </summary>
        /// <param name="loginDto">The data transfer object containing user credentials for authentication.</param>
        /// <returns>
        /// Returns a user with a generated token upon successful authentication. 
        /// If authentication fails, it returns a 401 error response with an appropriate error message.
        /// </returns>
        [HttpPost]
        //[AllowAnonymous] 
        public async Task<IActionResult> Login([FromBody]AuthenticateDto loginDto)
        {
            try
            {
                var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(loginDto);
                if (userCredentials == null) { return ResponseFactory.CreateErrorResponse(401, "Wrong credentials!"); } 

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
                return ResponseFactory.CreateErrorResponse(401, $"System error: {ex.Message}");
            }
        }
    }
}




