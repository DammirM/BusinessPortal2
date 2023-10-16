using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Models.DTO
{
    public class RegisterPersonalDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
