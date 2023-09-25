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
    ///Provee un endpoint para operaciones CRUD en trabajos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class TrabajosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="TrabajosController"/>.
        /// </summary>
        /// <param name="unitOfWork">Provee mecanismos para interactuar con los datos .</param>
        public TrabajosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Devuelve todos los Trabajos.
        /// </summary>
        /// <returns>Un listado de Trabajos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trabajos = await _unitOfWork.WorkRepository.GetAll();
            int pageToShow = 1;
            if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }
            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            var paginateTrabajos = PaginateHelper.Paginate(trabajos, pageToShow, url);

            return ResponseFactory.CreateSuccessResponse(200, paginateTrabajos);
        }

        /// <summary>
        /// Devuelve un trabajo según su ID.
        /// </summary>
        /// <param name="id">El ID del trabajo a devolver.</param>
        /// <returns>Devuelve trabajo si se lo encuentra. Sino, devuelve 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var trabajo = await _unitOfWork.WorkRepository.Get(id);
            if (trabajo == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado o borrado.");
            }

            return ResponseFactory.CreateSuccessResponse(200, trabajo);
        }

        /// <summary>
        /// Registra un nuevo trabajo.
        /// </summary>
        /// <param name="dto">El DTO conteniendo el registro del trabajo.</param>
        /// <returns>Mensaje de éxito si se registró. Sino, 409.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(TrabajosDto dto)
        {
            var trabajo = new Trabajos(dto);
            await _unitOfWork.WorkRepository.Insert(trabajo);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Trabajo registrado correctamente.");
        }

        /// <summary>
        /// Actualiza información del trabajo.
        /// </summary>
        /// <param name="id">ID del trabajo a actualizar.</param>
        /// <param name="dto">El DTO conteniendo información del trabajo a actualizar.</param>
        /// <returns>Mensaje de éxito si se actualizó. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, TrabajosDto dto)
        {
            var result = await _unitOfWork.WorkRepository.Update(new Trabajos(dto, id));
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado o borrado."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Trabajo actualizado correctamente.");
        }

        /// <summary>
        ///Borra un trabajo.
        /// </summary>
        /// <param name="id">ID del trabajo a eliminar.</param>
        /// <returns>Mensaje de éxito si se borró. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _unitOfWork.WorkRepository.Delete(id);
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Trabajo borrado exitosamente.");
        }
    }
}
