using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;

namespace alkemyumsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        [Route("api/getUsuario")]
        public ActionResult<LoginDto> GetUsuario(int id)
        {
            Login usuario = new Login();
            if (id == 1)
            {
                LoginDto usuarioDTO = new LoginDto()
                {
                    Id = usuario.Id,
                    Name = usuario.Name
                };
            return Ok(usuarioDTO);
            }
            return NotFound("User not found");
        }
    }
}


