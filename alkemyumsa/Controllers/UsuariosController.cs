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
    //[AllowAnonymous]
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
                return NotFound(new { message = "Usuario no encontrado o borrado." });
            }

            var userDto = UserMapper.MapToDto(user);
            return Ok(userDto);
        }

        // Registrar un nuevo usuario
        // POST: api/usuarios
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
           
            var user = new Usuarios(dto);
            await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.Complete(); // ejecuta la query, registra los cambios. Los guarda.

            return Ok(new { message = "Usuario registrado exitosamente." }); ;
        }

        // Actualizar un usuario
        // PUT: api/usuarios/{id}
        [Authorize(Policy = "Administrador")]
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update( [FromRoute] int id, RegisterDto dto)
        {
            

            var result = await _unitOfWork.UserRepository.Update(new Usuarios(dto, id));
            if (result == false) { return NotFound(new { message = "Usuario no encontrado o borrado." }); }


            await _unitOfWork.Complete();

            return Ok(new { message = "Usuario actualizado exitosamente." });
        }

        // Eliminar un usuario
        // DELETE: api/usuarios/{id}
        [Authorize(Policy = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _unitOfWork.UserRepository.Delete(id);
            if (result == false) { return NotFound(new { message = "Usuario no encontrado." }); }

            await _unitOfWork.Complete();

            return Ok(new { message = "Usuario borrado exitosamente." });
        }
    }
}
