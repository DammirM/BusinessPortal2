namespace BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO
{
    public class LeaveRequestCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; }
        public string ApprovalState { get; set; }

        public int LeaveTypeId { get; set; }
        public int PersonalId { get; set; }

    }
}
