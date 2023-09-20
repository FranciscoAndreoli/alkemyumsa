using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Infraestructure
{
    public class ApiSuccessResponse
    {
        public int Status { get; set; }
        public object? Data { get; set; }
    }
}
