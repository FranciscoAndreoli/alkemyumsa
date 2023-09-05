using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace alkemyumsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Simular base de datos en memoria
        private static List<Login> login = new()
        {
            new Login { Id = 1, Name = "Juan", Password = "123"},
            new Login { Id = 2, Name = "Maria", Password = "1234"}
        };


        /// <summary>
        /// Devuelve todos los usuarios
        /// </summary>
        /// <returns> todos los usuarios</returns>
        [HttpGet("getUsuarios")]
        public ActionResult<LoginDto> GetUsuarios()
        {
            var loginDto = login.Select(e => new LoginDto
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
            return Ok(loginDto);
        }
       

        /// <summary>
        /// Devuelve un usuario, segun ID
        /// </summary>
        /// <param name="id"> Entero</param>
        /// <returns>Un usuario</returns>
        [HttpGet("getUsuario/{id}")]
        public ActionResult<LoginDto> GetUsuario(int id)
        {
            try
            {
                var usuario = login.FirstOrDefault(e => e.Id == id);
                if (usuario == null)
                {
                    return NotFound();
                }
                var LoginDto = new LoginDto
                {
                    Id = usuario.Id,
                    Name = usuario.Name,

                };

                return Ok(LoginDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error del sistema: {ex.Message}");
            }
        }



        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="loginDto">DTO de Login</param>
        /// <returns>Devuelve URL del recurso creado.</returns>
        [HttpPost("postUsuario")]
        public ActionResult<LoginDto> CreateUsuario(LoginDto loginDto)
        {
            try
            {
                var usuario = new Login
                {
                    Id = login.Max(e => e.Id) + 1, // Generating new Id
                    Name = loginDto.Name
                };

                login.Add(usuario);

                loginDto.Id = usuario.Id;

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, loginDto); //Es una buena practica que POST luego de crear un recurso debe devolver un estado 201 y proporcionar una URL al recurso creado. Eso es lo que sucede en este return
            }
            catch (Exception ex)
            {
                return BadRequest($"Error del sistema: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza o reemplaza un usuario
        /// </summary>
        /// <param name="id"> integer </param>
        /// <param name="loginDto">DTO de Login</param>
        /// <returns>Retorna not found si no se encontró usuario. Si se encontró, no retorna contenido</returns>
        [HttpPut("putUsuario/{id}")]
        public ActionResult<LoginDto> UpdateUsuario(int id, LoginDto loginDto)
        {
            var usuario = login.FirstOrDefault(e => e.Id == id); //devolverá el primer elemento que cumpla con la condición. Sino, devuelve null.
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Id = loginDto.Id;
            usuario.Name = loginDto.Name;

            return NoContent();
        }


        /// <summary>
        /// Borra usuario.
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns>Retorna not found si no se encontró usuario. Si se encontró, no retorna contenido</returns>
        [HttpDelete("deleteteUsuario/{id}")]
        public ActionResult<LoginDto> DeleteUsuario(int id)
        {
            var usuario = login.FirstOrDefault(e => e.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            login.Remove(usuario);

            return NoContent();
        }
    }
}




