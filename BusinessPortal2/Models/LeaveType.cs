using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessPortal2.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LeaveName { get; set; }
        [Required]
        public int LeaveDays { get; set; }

        public int PersonalId { get; set; }
        public Personal Personal { get; set; } // Add this navigation property

    }
}
