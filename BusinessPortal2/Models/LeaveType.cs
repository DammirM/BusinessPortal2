using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessPortal2.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public int LeaveDays { get; set; }
        public ICollection<LeaveRequest> leaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
