using System.Net;

namespace MinimalAPI_BookStore.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public Boolean IsSuccess { get; set; } = false;
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public List<string> ErrorMessages { get; set; }
    }
}
