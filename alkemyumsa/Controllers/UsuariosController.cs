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
    /// Provides endpoints for CRUD operations on users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //  Todos los endpoint requieren autenticación el JWT.
    //[AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="UsuariosController"/> class.
        /// </summary>
        /// <param name="unitOfWork">Provides mechanisms to interact with the data source.</param>
        public UsuariosController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

        }


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of users.</returns>
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
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, a 404 error.</returns>
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
        /// Registers a new user.
        /// </summary>
        /// <param name="dto">The data transfer object containing user registration details.</param>
        /// <returns>A success message upon successful registration; otherwise, an error message.</returns>
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
            await _unitOfWork.Complete(); // ejecuta la query, registra los cambios. Los guarda.

            return ResponseFactory.CreateSuccessResponse(200, "User successfully registered.");
        }

        /// <summary>
        /// Updates a user's details.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="dto">The data transfer object containing the updated user details.</param>
        /// <returns>A success message upon successful update; otherwise, an error message.</returns>
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
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A success message upon successful deletion; otherwise, an error message.</returns>
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
