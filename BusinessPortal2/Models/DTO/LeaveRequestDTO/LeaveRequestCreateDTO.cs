namespace BusinessPortal2.Models.DTO.LeaveRequestDTO
{
    public class LeaveRequestCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; } = DateTime.Now;
        public string ApprovalState { get; set; } = "Pending";

        public int LeaveTypeId { get; set; }
        public int PersonalId { get; set; }

    }
}
