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
    /// Provides endpoints for CRUD operations on projects.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //  Todos los endpoint requieren autenticación el JWT.
    //[AllowAnonymous]
    public class ProyectosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProyectosController"/> class.
        /// </summary>
        /// <param name="unitOfWork">Provides mechanisms to interact with the data source.</param>
        public ProyectosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        /// <returns>A list of projects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proyectos = await _unitOfWork.ProjectRepository.GetAll();
            int pageToShow = 1;

            if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }

            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            //var ProyectosDto = projects.Select(proyectos => ProjectMapper.MapToDto(proyectos)).ToList();
            var paginateProyectos = PaginateHelper.Paginate(proyectos, pageToShow, url);


            return ResponseFactory.CreateSuccessResponse(200, paginateProyectos);
        }

        /// <summary>
        /// Retrieves a proyecto by their ID.
        /// </summary>
        /// <param name="id">The ID of the proyecto to retrieve.</param>
        /// <returns>The proyecto if found; otherwise, a 404 error.</returns>
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
        /// Registers a new proyecto.
        /// </summary>
        /// <param name="dto">The data transfer object containing proyecto registration details.</param>
        /// <returns>A success message upon successful registration; otherwise, an error message.</returns>
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
            await _unitOfWork.Complete(); // ejecuta la query, registra los cambios. Los guarda.

            return ResponseFactory.CreateSuccessResponse(200, "proyecto registrado correctamente.");
        }

        /// <summary>
        /// Updates a proyecto's details.
        /// </summary>
        /// <param name="id">The ID of the proyecto to update.</param>
        /// <param name="dto">The data transfer object containing the updated proyecto details.</param>
        /// <returns>A success message upon successful update; otherwise, an error message.</returns>
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
        /// Deletes a proyecto.
        /// </summary>
        /// <param name="id">The ID of the proyecto to delete.</param>
        /// <returns>A success message upon successful deletion; otherwise, an error message.</returns>
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
