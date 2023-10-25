namespace BusinessPortal2.Models.DTO.LeaveRequestDTO
{
    public class LeaveRequestUpdateDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; } = DateTime.Now;
        public string ApprovalState { get; set; }
        public int PersonalId { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
