using BusinessPortal2.Data;
using BusinessPortal2.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Services
{
    public class LeaveRequestRepo : ILeaveRequestRepo
    {
        private readonly PersonaldataContext _context;

        public LeaveRequestRepo(PersonaldataContext context)
        {
            _context= context;
        }

        public async Task<LeaveRequest> CreateLeaveRequuest(LeaveRequest LeaveRequest)
        {
            var request = await _context.leaveRequests.AddAsync(LeaveRequest);
            await _context.SaveChangesAsync();
            return request.Entity;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll(int id)
        {
            return await _context.leaveRequests
                .Where(leaveRequest => leaveRequest.PersonalId  == id)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetById(int id, int personalId)
        {
            return await _context.leaveRequests.Where(leaveRequest => leaveRequest.Id == id)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
