using Newtonsoft.Json;
using System.Text;
using WebApplicationBusinessPortal2.Models;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;

namespace WebApplicationBusinessPortal2.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientService _httpClientService;

        public AppResponse AppResponse { get; set; }

        public BaseService(IHttpClientService httpClientService)
        {
            AppResponse = new AppResponse();
            _httpClientService = httpClientService;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.Add("Accept", "application/json");
                httpRequestMessage.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case ApiType.GET:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                    case ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage response = await _httpClientService.Client.SendAsync(httpRequestMessage);

                var apiResponse = await response.Content.ReadAsStringAsync();
                var apiResponseObject = JsonConvert.DeserializeObject<T>(apiResponse);
                return apiResponseObject;   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
