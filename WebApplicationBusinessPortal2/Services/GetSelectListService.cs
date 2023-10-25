using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;
using WebApplicationBusinessPortal2.Models;

namespace WebApplicationBusinessPortal2.Services
{
    public class GetSelectListService : BaseService, IGetSelectListService
    {
        private readonly IHttpClientService _httpClientService;

        public GetSelectListService(IHttpClientService httpClientService) : base(httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<T> GetSelectListAsync<T>(string endpoint)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + endpoint,
                AccessToken = ""
            });
        }
    }
}
