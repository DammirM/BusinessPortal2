namespace WebApplicationBusinessPortal2.Models
{
    public class AppResponse
    {
        public AppResponse()
        {
            Errors = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> Errors { get; set; }
    }
}
