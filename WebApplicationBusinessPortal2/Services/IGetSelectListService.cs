namespace WebApplicationBusinessPortal2.Services
{
    public interface IGetSelectListService
    {
        Task<T> GetSelectListAsync<T>(string endpoint);
    }
}
