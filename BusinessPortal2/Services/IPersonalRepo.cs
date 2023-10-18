using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Services
{
    public interface IPersonalRepo
    {
        public Task<Personal> RegisterPersonal(Personal personal);
        public Task Login();
        public Task DeletePersonal(Personal personal);
        public Task<Personal> GetPersonalById(int id);
        public Task<IEnumerable<Personal>> GetAllPersonal();
        public Task UpdatePersonal(Personal personal);
    }
}
