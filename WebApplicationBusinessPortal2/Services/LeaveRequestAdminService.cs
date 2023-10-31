using BusinessPortal2.Models;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;
using WebApplicationBusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

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

        public async Task<T> CreateLeveType<T>(LeaveTypeCreateDTO leaveDTO)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = leaveDTO,
                Url = _httpClientService.Client.BaseAddress + "leavetypes/create/forall",
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

        public async Task<T> DeleteLeaveTypeByNameAsync<T>(string name)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = _httpClientService.Client.BaseAddress + $"leavetypes/deleteByName/{name}",
                AccessToken = ""
            });
        }

        public async Task<T> DeletePersonalAsync<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = _httpClientService.Client.BaseAddress + $"personal/delete/{Id}",
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

        public async Task<T> GetLeaveTypeAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"leavetypes/getall",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveTypeByIdtAdminAsync<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"leavetypes/get/{Id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveTypeByPersonalId<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"leavetypes/get/All/{Id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetPersonalAdminAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"personal/getall",
                AccessToken = ""
            });
        }

        public async Task<T> GetPersonalByIdAdminAsync<T>(int Id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"personal/get/{Id}",
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

        public async Task<T> GetTotalDaysApprovedFromAllLeaveTypes<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + "leavetypes/total/leavetime/hours",
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


        public async Task<T> UpdateLeaveTypeAdminAsync<T>(LeaveTypeUpdateDTO leaveDTO)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = leaveDTO,
                Url = _httpClientService.Client.BaseAddress + "leavetypes/update",
                AccessToken = ""
            });
        }

        public async Task<T> UpdatePersonalAdminAsync<T>(PersonalUpdateDTO personalDTO)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = personalDTO,
                Url = _httpClientService.Client.BaseAddress + "personal/update",
                AccessToken = ""
            });
        }
    }
}
