using BusinessPortal2.Data;
using BusinessPortal2.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Services
{
    public class LeaveRequestAdminRepo : LeaveRequestRepo
    {
        private readonly PersonaldataContext _context;
        public LeaveRequestAdminRepo(PersonaldataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var requestToDelete = await _context.leaveRequests
                .FirstOrDefaultAsync(leaveRequest => leaveRequest.Id == id);
            if(requestToDelete != null)
            {
                _context.Remove(requestToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll()
        {
            return await _context.leaveRequests
                .Include(leaveRequest => leaveRequest.personal)
                .Include(leaveRequest => leaveRequest.leaveType)
                .ToListAsync();
        }

        public async Task Update(LeaveRequest request)
        {
            _context.leaveRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<LeaveRequest> GetById(int id)
        {
            return await _context.leaveRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
