using BusinessPortal2.Data;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using BusinessPortal2.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<Personal> GetPersonalById(int personalId)
        {
            return await context.personals.FirstOrDefaultAsync(personal => personal.Id == personalId);
        }

        public async Task<LoginResult> Login(LoginPersonalDTO personal)
        {
            LoginResult loginResult = new LoginResult() { User = null, IsUserValid = false };

            var userToLogin = await context.personals
                .FirstOrDefaultAsync(email => email.Email == personal.Email);

            if(userToLogin != null)
            {
                var passwordEquality = BCrypt.Net.BCrypt.Verify(personal.Password, userToLogin.Password);
                if (passwordEquality)
                {
                    loginResult.IsUserValid = true;
                    loginResult.User = userToLogin;

                    return loginResult;
                }
            }

            return loginResult;
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
