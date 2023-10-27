namespace WebApplicationBusinessPortal2.Services
{
    public interface ILeaveTypeService
    {
        Task<T> GetAllLeaveRequestByPersonId<T>(int personalId);
    }
}
