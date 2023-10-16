using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Services
{
    public interface IPersonalRepo
    {
        public Task<Personal> Register(Personal personal);
        public Task Login();
        public Task Delete(Personal personal);
        public Task<Personal> GetPersonalById(int id);
        public Task<IEnumerable<Personal>> GetAll();
    }
}
