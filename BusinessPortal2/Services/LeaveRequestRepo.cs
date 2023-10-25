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

        public async Task<LeaveRequest> CreateLeaveRequest(LeaveRequest LeaveRequest)
        {
            var request = await _context.leaveRequests.AddAsync(LeaveRequest);
            await _context.SaveChangesAsync();
            return request.Entity;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequest(int id)
        {
            return await _context.leaveRequests
                .Where(leaveRequest => leaveRequest.PersonalId  == id)
                .Include(leaveRequest =>leaveRequest.leaveType)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int leaveRequestId, int personalId)
        {
            return await _context.leaveRequests
                .Where(leaveRequest => leaveRequest.PersonalId == personalId)
                .Include(leaveRequest => leaveRequest.leaveType)
                .FirstOrDefaultAsync(leaveRequest => leaveRequest.Id == leaveRequestId);
        }

        public async Task<bool> DeleteLeaveRequest(int id, int personalId)
        {
            var requestToDelete = await _context.leaveRequests
                .Where(leaveRequest => leaveRequest.PersonalId == personalId)
                .FirstOrDefaultAsync(leaveRequest => leaveRequest.Id == id);
            if (requestToDelete != null)
            {
                _context.Remove(requestToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
