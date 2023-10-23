namespace WebApplicationBusinessPortal2.Models.ConfigurationModels
{
    public class ApiSettings
    {
        public string BaseAddress { get; set; }

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
