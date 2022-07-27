using System.Net;

namespace Identity.Application.Dtos.Common
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    }
}
