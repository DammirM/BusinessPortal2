using BusinessPortal2.Models.DTO.LeaveRequestDTO;

namespace BusinessPortal2.Models.DTO.LeaveTypeDTO
{
    public class LeaveTypeSimpleReadDTO
    {
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public int LeaveDays { get; set; }

        public List<LeaveRequestReadDTO> leaveRequests { get; set; } = new List<LeaveRequestReadDTO>();
    }
}
