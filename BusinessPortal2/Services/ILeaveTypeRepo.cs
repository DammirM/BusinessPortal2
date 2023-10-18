using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;

namespace BusinessPortal2.Services
{
    public interface ILeaveTypeRepo
    {
        Task<LeaveType> Create(LeaveTypeCreateDTO leaveTypeCreateDTO);
        Task<IEnumerable<LeaveType>> GetAll();

        Task<LeaveType> GetById(int leaveTypeId);

        Task<LeaveType> Update(LeaveTypeUpdateDTO leaveTypeUpdateDTO);

        Task Delete(int leaveTypeId);
    }
}
