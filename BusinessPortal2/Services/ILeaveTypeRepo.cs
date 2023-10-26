using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;

namespace BusinessPortal2.Services
{
    public interface ILeaveTypeRepo
    {
        Task<LeaveType> CreateLeaveType(LeaveType leaveType);
        Task<LeaveType> CreateLeaveTypeForAll(LeaveType leaveType);
        Task<IEnumerable<LeaveType>> GetAllLeaveType();

        Task<LeaveType> GetLeaveTypeById(int leaveTypeId);
        Task<IEnumerable<LeaveType>> GetAllLeaveTypeByPersonalId(int personalId);

        Task<LeaveType> UpdateLeaveType(LeaveType leaveType);
        Task<LeaveType> UpdateLeaveByNameType(LeaveType leaveType, string name);

        Task<bool> DeleteLeaveType(int leaveTypeId);
        Task<bool> DeleteLeaveTypeByName(string name);
    }
}
