using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;
using WebApplicationBusinessPortal2.Models;

namespace WebApplicationBusinessPortal2.Services
{
    public class AccessService : BaseService, IAccessService
    {

        private readonly IHttpClientService _httpClientService;

        public AccessService(IHttpClientService httpClientService) : base(httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<T> LoginAsync<T>(LoginPersonalDTO l_Personal_DTO)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = l_Personal_DTO,
                Url = _httpClientService.Client.BaseAddress + "personal/login",
                AccessToken = ""
            });
        }

        public async Task<T> RegisterAsync<T>(RegisterPersonalDTO r_Personal_DTO)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = r_Personal_DTO,
                Url = _httpClientService.Client.BaseAddress + "personal/create",
                AccessToken = ""
            });
        }
    }
}
