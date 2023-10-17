using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public interface ILeaveTypeRepo
    {

        Task<LeaveType> CreateLeave(LeaveType leaveType);
        Task<IEnumerable<LeaveType>> GetAll();

        Task<LeaveType> GetById(int id);

        Task<LeaveType> UpdateLeave(int id, LeaveType tr);

        Task DeleteLeave(int personalId);

    }
}
