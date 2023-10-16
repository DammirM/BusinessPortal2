using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public interface ILeaveTypeRepository
    {
        Task<IEnumerable<LeaveType>> GetAll();

        Task<LeaveType> GetById(int id);

        Task<LeaveType> UpdateLeave(int id, LeaveType tr);

        Task<LeaveType> DeleteLeave(int personalId);

    }
}
