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
    ///Provee un endpoint para operaciones CRUD en servicios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class ServiciosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ServiciosController"/>.
        /// </summary>
        /// <param name="unitOfWork">Provee mecanismos para interactuar con los datos .</param>
        public ServiciosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Devuelve todos los Servicios.
        /// </summary>
        /// <returns>Un listado de Servicios.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicios = await _unitOfWork.ServiceRepository.GetAll();
            int pageToShow = 1;
            if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }
            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            var paginateServicios = PaginateHelper.Paginate(servicios, pageToShow, url);

            return ResponseFactory.CreateSuccessResponse(200, paginateServicios);
        }

        /// <summary>
        /// Devuelve un servicio según su ID.
        /// </summary>
        /// <param name="id">El ID del servicio a devolver.</param>
        /// <returns>Devuelve servicio si se lo encuentra. Sino, devuelve 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var servicio = await _unitOfWork.ServiceRepository.Get(id);
            if (servicio == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado o borrado.");
            }

            return ResponseFactory.CreateSuccessResponse(200, servicio);
        }

        /// <summary>
        /// Trae un listado de servicios activos.
        /// </summary>
        /// <returns>Mensaje de éxito si se devolvió un listado. Sino, 404.</returns>
        [HttpGet]
        [Route("GetActiveServices")]
        public async Task<IActionResult> GetActiveServices()
        {
            var serviciosActivos = await _unitOfWork.ServiceRepository.GetActiveServices();
            if (serviciosActivos.Count == 0)
            {
                return ResponseFactory.CreateErrorResponse(404, "Servicios no encontrados o borrados.");
            }

            return ResponseFactory.CreateSuccessResponse(200, serviciosActivos);
        }

        /// <summary>
        /// Registra un nuevo servicio.
        /// </summary>
        /// <param name="dto">El DTO conteniendo el registro del servicio.</param>
        /// <returns>Mensaje de éxito si se registró. Sino, 409.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ServiciosDto dto)
        {
            if (await _unitOfWork.ServiceRepository.Check(dto.Descr))
            {
                return ResponseFactory.CreateErrorResponse(409, $"Este servicio ya existe: {dto.Descr}");
            }
            var servicio = new Servicios(dto);
            await _unitOfWork.ServiceRepository.Insert(servicio);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Servicio registrado correctamente.");
        }

        /// <summary>
        /// Actualiza información del servicio.
        /// </summary>
        /// <param name="id">ID del servicio a actualizar.</param>
        /// <param name="dto">El DTO conteniendo información del servicio a actualizar.</param>
        /// <returns>Mensaje de éxito si se actualizó. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, ServiciosDto dto)
        {
            var result = await _unitOfWork.ServiceRepository.Update(new Servicios(dto, id));
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado o borrado."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Servicio actualizado correctamente.");
        }

        /// <summary>
        ///Borra un servicio.
        /// </summary>
        /// <param name="id">ID del servicio a eliminar.</param>
        /// <returns>Mensaje de éxito si se borró. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _unitOfWork.ServiceRepository.Delete(id);
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Servicio borrado exitosamente.");
        }
    }
}
