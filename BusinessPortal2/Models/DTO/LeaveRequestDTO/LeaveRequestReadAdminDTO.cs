using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

namespace BusinessPortal2.Models.DTO.LeaveRequestDTO
{
    public class LeaveRequestReadAdminDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; }
        public string ApprovalState { get; set; }

        public LeaveTypeSimpleReadDTO leaveType { get; set; }
        public PersonalReadDTO personal { get; set; }

    }
}
