using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public interface ILeaveRequestRepo
    {
        Task<LeaveRequest> CreateLeaveRequuest(LeaveRequest LeaveRequest);
        Task<IEnumerable<LeaveRequest>> GetAll(int id);

        Task<LeaveRequest> GetById(int id, int personalId);

    }
}
