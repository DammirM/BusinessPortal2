using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Models.DTO.LeaveTypeDTO
{
    public class LeaveTypeUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string LeaveName { get; set; }
        [Required]
        public int LeaveDays { get; set; }
        public int PersonalId { get; set; }
    }
}
