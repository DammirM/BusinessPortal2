using BusinessPortal2.Models.DTO.PersonalDTO;

namespace WebApplicationBusinessPortal2.Services
{
    public interface IAccessService
    {
        Task<T> RegisterAsync<T>(RegisterPersonalDTO r_Personal_DTO);
        Task<T> LoginAsync<T>(LoginPersonalDTO r_Personal_DTO);
    }
}
