using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public interface ILeaveRequest
    {
        Task<LeaveRequest> CreateLeaveRequuest(LeaveRequest LeaveRequest);
        Task<IEnumerable<LeaveRequest>> GetAll();

        Task<LeaveRequest> GetById(int id);

        Task<LeaveRequest> UpdateLeaveRequest(int id, LeaveRequest LeaveRequest);

        Task<LeaveRequest> DeleteLeaveRequest(int personalId);

    }
}
