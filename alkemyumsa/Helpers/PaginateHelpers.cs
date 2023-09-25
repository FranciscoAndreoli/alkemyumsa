using alkemyumsa.DTOs;

namespace alkemyumsa.Helpers
{
    /// <summary>
    /// Clase auxiliar para la paginación de listas.
    /// </summary>
    public static class PaginateHelper
    {
        /// <summary>
        /// Pagina una lista de elementos basándose en la página actual y la URL proporcionada.
        /// </summary>
        /// <typeparam name="T">El tipo de elementos dentro de la lista a paginar.</typeparam>
        /// <param name="itemsToPaginate">La lista de elementos a paginar.</param>
        /// <param name="currentPage">La página actual para la paginación.</param>
        /// <param name="url">La URL base que se utilizará para generar las URLs de las páginas anterior y siguiente.</param>
        /// <returns>Retorna un objeto PaginateDataDto que contiene la información paginada y las URLs de las páginas anterior y siguiente.</returns>
        public static PaginateDataDto<T> Paginate<T>(List<T> itemsToPaginate, int currentPage, string url)
        {
            int pageSize = 10;
            var totalItems = itemsToPaginate.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginateItems = itemsToPaginate.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var prevUrl = currentPage > 1 ? $"{url}?page={currentPage - 1}" : null;
            var nextUrl = currentPage < totalPages ? $"{url}?page={currentPage + 1}" : null;

            return new PaginateDataDto<T>()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                PrevUrl = prevUrl,
                NextUrl = nextUrl,
                Items = paginateItems
            };
        }
    }
}
