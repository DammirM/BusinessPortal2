using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;

namespace WebApplicationBusinessPortal2.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; }

        public string Url { get; set; }
        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
