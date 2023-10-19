namespace BusinessPortal2.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public int LeaveDays { get; set; }

        public List<LeaveRequest> leaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
