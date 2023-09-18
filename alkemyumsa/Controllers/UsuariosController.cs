using alkemyumsa.Entities;
using alkemyumsa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using alkemyumsa.DTOs;
using alkemyumsa.Helpers;
using Microsoft.OpenApi.Validations;

namespace alkemyumsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //  Todos los endpoint requieren autenticación el JWT.
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsuariosController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

        }

        // Obtener todos los usuarios
        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetAll() 
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var userDtos = users.Select(user => UserMapper.MapToDto(user)).ToList();
            return Ok(userDtos);
        }

        // Obtener un usuario por ID
        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var user = await _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found or deleted." });
            }

            var userDto = UserMapper.MapToDto(user);
            return Ok(userDto);
        }

        // Registrar un nuevo usuario
        // POST: api/usuarios
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var user = new Usuarios(dto);
            await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.Complete(); // ejecuta la query, registra los cambios. Los guarda.

            return Ok(new { message = "User registered successfully." }); ;
        }

        // Actualizar un usuario
        // PUT: api/usuarios/{id}
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update( [FromRoute] int id, RegisterDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await _unitOfWork.UserRepository.Update(new Usuarios(dto, id));

            await _unitOfWork.Complete();

            return Ok(new { message = "User updated successfully." });
        }

        // Eliminar un usuario
        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _unitOfWork.UserRepository.Delete(id);

            await _unitOfWork.Complete();

            return Ok(new { message = "User deleted successfully." });
        }
    }
}
