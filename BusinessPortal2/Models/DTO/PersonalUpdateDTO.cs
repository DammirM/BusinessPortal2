﻿namespace BusinessPortal2.Models.DTO
{
    public class PersonalUpdateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; } = false;
    }
}
