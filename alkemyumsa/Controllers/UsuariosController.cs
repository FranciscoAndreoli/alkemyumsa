using alkemyumsa.Entities;
using alkemyumsa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetAll() 
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users;
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
