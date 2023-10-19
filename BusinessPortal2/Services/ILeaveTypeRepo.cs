using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;

namespace BusinessPortal2.Services
{
    public interface ILeaveTypeRepo
    {
        Task<LeaveType> CreateLeaveType(LeaveType leaveType);
        Task<IEnumerable<LeaveType>> GetAllLeaveType();

        Task<LeaveType> GetLeaveTypeById(int leaveTypeId);

        Task<LeaveType> UpdateLeaveType(LeaveType leaveType);

        Task<bool> DeleteLeaveType(int leaveTypeId);
    }
}
