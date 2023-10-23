namespace WebApplicationBusinessPortal2.Services
{
    public interface ILeaveRequestService
    {
        Task<T> GetLeaveRequestAsync<T>(int personalId);
        Task<T> GetLeaveRequestByIdAsync<T>(int personalId, int leaveRequestId);
        Task<T> CreateLeaveRequestAsync<T>(T leaveRequest);
        Task<T> DeleteLeaveRequestAsync<T>(int personalId, int leaveRequestId);
    }
}
