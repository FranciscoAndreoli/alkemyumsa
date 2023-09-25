using alkemyumsa.Entities;
using alkemyumsa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using alkemyumsa.DTOs;
using alkemyumsa.Helpers;
using Microsoft.OpenApi.Validations;
using alkemyumsa.Infraestructure;

namespace alkemyumsa.Controllers
{
    /// <summary>
    /// Provee ENDPOINTS para realizar operaciones CRUD de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    //[AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Inicializa nueva instancia de  <see cref="UsuariosController"/>.
        /// </summary>
        /// <param name="unitOfWork">Provee mecanismos para interactuar con los datos de la base.</param>
        public UsuariosController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Devuelve todos los usuarios.
        /// </summary>
        /// <returns>Un listado de usuarios.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            int pageToShow = 1;
            if(Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow);  }
            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            var userDtos = users.Select(user => UserMapper.MapToDto(user)).ToList();
            var paginateUsers = PaginateHelper.Paginate(userDtos, pageToShow, url);
            
            return ResponseFactory.CreateSuccessResponse(200, userDtos);
        }

        /// <summary>
        ///Devuelve un usuario por ID.
        /// </summary>
        /// <param name="id">El ID del usuario a devolver.</param>
        /// <returns>El usuario si se encuentra. Sino, 404</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var user = await _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "User not found or deleted.");
            }
            var userDto = UserMapper.MapToDto(user);

            return ResponseFactory.CreateSuccessResponse(200, userDto);
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="dto">El DTO que contiene detalles del registro</param>
        /// <returns>Un mensaje de éxito si se registró correctamente. Sino, error.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _unitOfWork.UserRepository.Check(dto.Email))
            {
                return ResponseFactory.CreateErrorResponse(409, $"This user already exists: {dto.Email}");
            }
            var user = new Usuarios(dto);
            await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "User successfully registered.");
        }

        /// <summary>
        /// Actualiza los datos del usuario.
        /// </summary>
        /// <param name="id">El ID del usuario a actualizar.</param>
        /// <param name="dto">El DTO que contiene información del usuario a actualizar.</param>
        /// <returns>Un mensaje de éxito si se actualizó. Sino, error.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update( [FromRoute] int id, RegisterDto dto)
        {
            var result = await _unitOfWork.UserRepository.Update(new Usuarios(dto, id));
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "User not found or deleted."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Usuario updated succesfully.");
        }

        /// <summary>
        /// Borrar un usuario.
        /// </summary>
        /// <param name="id">El ID del usuario a borrar.</param>
        /// <returns>Un mensaje de éxito si se borró. Sino, error.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _unitOfWork.UserRepository.Delete(id);
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "User not found."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Usuario deleted succesfully");
        }
    }
}
