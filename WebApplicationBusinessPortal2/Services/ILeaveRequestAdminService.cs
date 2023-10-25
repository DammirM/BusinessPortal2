using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;

namespace WebApplicationBusinessPortal2.Services
{
    public interface ILeaveRequestAdminService
    {
        Task<T> GetLeaveRequestAdminAsync<T>();
        Task<T> GetPersonalLeaveRequestAdminAsync<T>(int Id);
        Task<T> GetLeaveRequestByIdAdminAsync<T>(int Id); 
        Task<T> CreateLeaveRequestAdminAsync<T>(LeaveRequestCreateDTO leaveRequest);
        Task<T> UpdateLeaveRequesAdminAsync<T>(LeaveRequestUpdateDTO leaveRequest);
        Task<T> DeleteLeaveRequestAdminAsync<T>(int Id);

        // LeaveType

        Task<T> CreateLeveType<T>(LeaveTypeCreateDTO leaveDTO);

        // GETALL PERSONAL

        Task<T> GetPersonalAdminAsync<T>();
    }
}
