using BusinessPortal2.Models;
using WebApplicationBusinessPortal2.Models;

namespace WebApplicationBusinessPortal2.Services
{
    public interface IBaseService
    {
        AppResponse AppResponse { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
