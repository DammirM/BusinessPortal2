using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;
using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Models.DTO.LeaveTypeDTO
{
    public class LeaveTypeReadDTO
    {
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public int LeaveDays { get; set; }
        public int PersonalId { get; set; }
        public List<LeaveRequestReadDTO> leaveRequests { get; set; } = new List<LeaveRequestReadDTO>();
    }
}
