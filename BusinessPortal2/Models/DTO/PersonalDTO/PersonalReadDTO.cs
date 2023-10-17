namespace BusinessPortal2.Models.DTO.PersonalDTO
{
    public class PersonalReadDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; } = false;
    }
}
