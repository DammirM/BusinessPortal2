using BusinessPortal2.Models;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;
using WebApplicationBusinessPortal2.Models;

namespace WebApplicationBusinessPortal2.Services
{
    public class LeaveTypeService : BaseService, ILeaveTypeService
    {
        private readonly IHttpClientService _httpClientService;
        public LeaveTypeService(IHttpClientService httpClientService) : base(httpClientService)
        {
            this._httpClientService = httpClientService;
        }

        public async Task<T> GetAllLeaveRequestByPersonId<T>(int personId)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"get/All/{personId}",
                AccessToken = ""
            });
        }
    }
}
