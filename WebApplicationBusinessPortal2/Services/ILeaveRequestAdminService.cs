using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;

namespace WebApplicationBusinessPortal2.Services
{
    public interface ILeaveRequestAdminService
    {
        Task<T> GetLeaveRequestAdminAsync<T>();
        Task<T> GetPersonalLeaveRequestAdminAsync<T>(int Id); // GET BY PERSONALID
        Task<T> GetLeaveRequestByIdAdminAsync<T>(int Id); // GET BY LEAVEREQUESTID
        Task<T> CreateLeaveRequestAdminAsync<T>(LeaveRequestCreateDTO leaveRequest);
        Task<T> UpdateLeaveRequesAdminAsync<T>(LeaveRequestUpdateDTO leaveRequest); // UPDATE LEAVEREQUEST
        Task<T> DeleteLeaveRequestAdminAsync<T>(int Id);
    }
}
