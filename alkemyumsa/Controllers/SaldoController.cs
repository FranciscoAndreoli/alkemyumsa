using alkemyumsa.Entities;
using Microsoft.AspNetCore.Mvc;

namespace alkemyumsa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaldoController : ControllerBase
    {


        public SaldoController()
        {
        //El constructor del controlador generalmente se usa para inyección de dependencias.
        }

        [HttpGet]
        [Route("ConsultarSaldo")]
        public IActionResult GetSaldo(int saldo)
        {
            return Ok(saldo);
        }

        [HttpPost]
        [Route("AgregarSaldo")]
    
        // Con FromBody se pasa el parametro por el cuerpo de la solicitud.
        public IActionResult PostSaldo( [FromBody ] bool activo)
        {
            return Ok(activo);
        }
    }
}