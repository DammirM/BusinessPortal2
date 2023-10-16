using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessPortal2.Models
{
    public class LeaveType
    {

        [Key]
        [ForeignKey("Personal")]
        public int PersonalId { get; set; }
        public int Vabb { get; set; }
        public int Sick { get; set; }
        public int Vacation { get; set; }

        // public Personal Personal { get; set; } // Add this navigation property


    }
}
