using BusinessPortal2.Data;
using BusinessPortal2.Models;

namespace BusinessPortal2.Services
{
    public class LeaveRequestRepo : ILeaveRequest
    {
        private readonly PersonaldataContext _context;

        public LeaveRequestRepo(PersonaldataContext context)
        {
            _context= context;
        }

        public Task<LeaveRequest> CreateLeaveRequuest(LeaveRequest LeaveRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> DeleteLeaveRequest(int personalId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> UpdateLeaveRequest(int id, LeaveRequest LeaveRequest)
        {
            throw new NotImplementedException();
        }
    }
}
