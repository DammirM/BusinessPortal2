using Azure.Core;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;
using WebApplicationBusinessPortal2.Models;
using WebApplicationBusinessPortal2.Models.ConfigurationModels;
using static WebApplicationBusinessPortal2.Models.ConfigurationModels.ApiSettings;

namespace WebApplicationBusinessPortal2.Services
{
    public class LeaveRequestService : BaseService, ILeaveRequestService
    {
        private readonly IHttpClientService _httpClientService;

        public LeaveRequestService(IHttpClientService httpClientService) :base(httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<T> CreateLeaveRequestAsync<T>(T leaveRequest)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = leaveRequest,
                Url = _httpClientService.Client.BaseAddress + "user/leaverequest/create",
                AccessToken = ""
            }); 
        }

        public async Task<T> DeleteLeaveRequestAsync<T>(int personalId, int leaveRequestId)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = _httpClientService.Client.BaseAddress + $"user/leaverequest/delete/{personalId}/{leaveRequestId}",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveRequestAsync<T>(int personalId)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"user/leaverequest/getall/{personalId}",
                AccessToken = ""
            });
        }

        public async Task<T> GetLeaveRequestByIdAsync<T>(int personalId, int leaveRequestId)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = _httpClientService.Client.BaseAddress + $"user/leaverequest/get/{personalId}/{leaveRequestId}",
                AccessToken = ""
            });
        }

    }
}
