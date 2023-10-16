using System.Net;

namespace BusinessPortal2.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public Object body { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
