using Microsoft.Extensions.Options;
using WebApplicationBusinessPortal2.Models.ConfigurationModels;

namespace WebApplicationBusinessPortal2.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ApiSettings _apiSettings;

        public HttpClient Client { get; }

        public HttpClientService(IOptions<ApiSettings> apiSettings)
        {

            _apiSettings = apiSettings.Value;
            Client = new HttpClient();
            Client.BaseAddress = new Uri(_apiSettings.BaseAddress);
        }
    }
}
