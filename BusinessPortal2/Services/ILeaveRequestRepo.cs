using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public interface ILeaveRequestRepo
    {
        Task<LeaveRequest> CreateLeaveRequest(LeaveRequest LeaveRequest);
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequest(int id);

        Task<LeaveRequest> GetLeaveRequestById(int id, int personalId);

    }
}
