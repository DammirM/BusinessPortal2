using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Models
{
    public class Personal
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool  isAdmin { get; set; } = false;

        public LeaveType leavetype { get; set; }
    }
}
