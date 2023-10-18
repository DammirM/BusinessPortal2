using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Models.DTO.LeaveTypeDTO
{
    public class LeaveTypeCreateDTO
    {
        [Required]
        public string LeaveName { get; set; }
        [Required]
        public int LeaveDays { get; set; }
        [Required]
        public int PersonalId { get; set; }
    }
}
