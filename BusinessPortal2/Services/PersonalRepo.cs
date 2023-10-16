using BusinessPortal2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Services
{
    public class PersonalRepo : IPersonalRepo
    {
        //private readonly DataContext context;
        //public PersonRepo(DataContext _context)
        //{
        //    this.context = _context;
        //}
        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Register(Personal personal)
        {
            throw new NotImplementedException();
        }
    }
}
