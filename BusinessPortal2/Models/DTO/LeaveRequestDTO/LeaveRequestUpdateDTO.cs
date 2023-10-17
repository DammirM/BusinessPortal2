namespace BusinessPortal2.Models.DTO.LeaveRequestDTO
{
    public class LeaveRequestUpdateDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; }
        public string ApprovalState { get; set; }
        public string LeaveTypeName { get; set; }
    }
}
