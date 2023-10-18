using BusinessPortal2.Data;
using BusinessPortal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Services
{
    public class PersonalRepo : IPersonalRepo
    {
        private readonly PersonaldataContext context;
        public PersonalRepo(PersonaldataContext _context)
        {
            this.context = _context;
        }
        public async Task DeletePersonal(Personal p)
        {
            context.personals.Remove(p);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Personal>> GetAllPersonal()
        {
            return await context.personals.ToListAsync();
        }

        public async Task<Personal> GetPersonalById(int id)
        {
            return await context.personals.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public async Task<Personal> RegisterPersonal(Personal p)
        {
            var personal = await context.personals.AddAsync(p);
            await context.SaveChangesAsync();
            return personal.Entity;
        }

        public async Task UpdatePersonal(Personal personal)
        {
            context.Update(personal);
            await context.SaveChangesAsync();
        }
    }
}
