using BusinessPortal2.Models;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;
using WebApplicationBusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;

namespace WebApplicationBusinessPortal2.Services
{
    public class LeaveRequestAdminService : BaseService, ILeaveRequestAdminService
    {

        private readonly IHttpClientService _httpClientService;

        public LeaveRequestAdminService(IHttpClientService httpClientService) : base(httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<T> CreateLeaveRequestAdminAsync<T>(LeaveRequestCreateDTO leaveRequest)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = leaveRequest,
                Url = _httpClientService.Client.BaseAddress + "admin/leaverequest/create",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteLeaveRequestAdminAsync<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = _httpClientService.Client.BaseAddress + $"admin/leaverequest/delete/{Id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveRequestAdminAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"admin/leaverequest/getall",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveRequestByIdAdminAsync<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"admin/leaverequest/get/{Id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetPersonalLeaveRequestAdminAsync<T>(int personalId)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"admin/leaverequest/getall/{personalId}",
                AccessToken = ""
            });
        }


        public async Task<T> UpdateLeaveRequesAdminAsync<T>(LeaveRequestUpdateDTO leaveRequest)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = leaveRequest,
                Url = _httpClientService.Client.BaseAddress + "admin/leaverequest/update",
                AccessToken = ""
            });
        }
    }
}
