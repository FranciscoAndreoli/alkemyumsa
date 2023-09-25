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
    ///Provee un endpoint para operaciones CRUD en proyectos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    //[AllowAnonymous]
    public class ProyectosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProyectosController"/>.
        /// </summary>
        /// <param name="unitOfWork">Provee mecanismos para interactuar con los datos .</param>
        public ProyectosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Devuelve todos los proyectos.
        /// </summary>
        /// <returns>Un listado de proyectos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proyectos = await _unitOfWork.ProjectRepository.GetAll();
            int pageToShow = 1;
            if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }
            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            var paginateProyectos = PaginateHelper.Paginate(proyectos, pageToShow, url);

            return ResponseFactory.CreateSuccessResponse(200, paginateProyectos);
        }

        /// <summary>
        /// Devuelve un proyecto según su ID.
        /// </summary>
        /// <param name="id">El ID del proyecto a devolver.</param>
        /// <returns>Devuelve proyecto si se lo encuentra. Sino, devuelve 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var proyecto = await _unitOfWork.ProjectRepository.Get(id);
            if (proyecto == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "proyecto not found or deleted.");
            }

            return ResponseFactory.CreateSuccessResponse(200, proyecto);
        }

        /// <summary>
        /// Devuelve un listado de proyectos, filtrados por estado.
        /// </summary>
        /// <param name="estado">Estado de los proyectos a devolver: Pendiente, Confirmado, Terminado.</param>
        /// <returns>Devuelve proyectos si se lo encuentra. Sino, devuelve 404.</returns>
        [HttpGet("GetProjectByStatus")]
        public async Task<IActionResult> GetProjects(string estado)
        {
            var proyectos = await _unitOfWork.ProjectRepository.GetProjects(estado);
            if (proyectos.Count == 0)
            {
                return ResponseFactory.CreateErrorResponse(404, "Proyectos no encontrados o borrados.");
            }

            return ResponseFactory.CreateSuccessResponse(200, proyectos);
        }


        /// <summary>
        /// Registra un nuevo proyecto.
        /// </summary>
        /// <param name="dto">El DTO conteniendo el registro del proyecto.</param>
        /// <returns>Mensaje de éxito si se registró. Sino, 409.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ProyectosDto dto)
        {
            if (await _unitOfWork.ProjectRepository.Check(dto.Nombre))
            {
                return ResponseFactory.CreateErrorResponse(409, $"Este proyecto ya existe: {dto.Nombre}");
            }
            var proyecto = new Proyectos(dto);
            await _unitOfWork.ProjectRepository.Insert(proyecto);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "proyecto registrado correctamente.");
        }

        /// <summary>
        /// Actualiza información del proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto a actualizar.</param>
        /// <param name="dto">El DTO conteniendo información del proyecto a actualizar.</param>
        /// <returns>Mensaje de éxito si se actualizó. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, ProyectosDto dto)
        {
            var result = await _unitOfWork.ProjectRepository.Update(new Proyectos(dto, id));
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "Proyecto no encontrado o borrado."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Proyecto actualizado correctamente.");
        }

        /// <summary>
        ///Borra un proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto a eliminar.</param>
        /// <returns>Mensaje de éxito si se borró. Sino, 404.</returns>
        [Authorize(Policy = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _unitOfWork.ProjectRepository.Delete(id);
            if (result == false) { return ResponseFactory.CreateErrorResponse(404, "proyecto not found."); }
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(200, "Proyecto deleted succesfully");
        }
    }
}
