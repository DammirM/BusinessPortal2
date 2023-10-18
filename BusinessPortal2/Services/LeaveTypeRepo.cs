using BusinessPortal2.Data;
using BusinessPortal2.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Services
{
    public class LeaveTypeRepo : ILeaveTypeRepo
    {
        private readonly PersonaldataContext _context;

        public LeaveTypeRepo(PersonaldataContext context)
        {
            _context = context;
        }

        public async Task<LeaveType> CreateLeaveType(LeaveType leaveType)
        {
            if (leaveType != null)
            {
                var register = await _context.leaveTypes.AddAsync(leaveType);
                await _context.SaveChangesAsync();
                return register.Entity;
            }
            return null;
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveType()
        {
            var leaveTypeAll = await _context.leaveTypes.ToListAsync();

            return leaveTypeAll;
        }

        public async Task<LeaveType> GetLeaveTypeById(int leaveTypeId)
        {
            var leaveTypeById = await _context.leaveTypes.FirstOrDefaultAsync(leaveType => leaveType.Id == leaveTypeId);
            
            return leaveTypeById;
        }

        public async Task<LeaveType> UpdateLeaveType(LeaveType leaveType)
        {
            if (leaveType != null)
            {
                _context.leaveTypes.Update(leaveType);
                await _context.SaveChangesAsync();
                return leaveType;
            }
            return null;
        }

        public async Task<bool> DeleteLeaveType(int leaveTypeId)
        {
            var leaveTypeToDelete = await _context.leaveTypes
                .FirstOrDefaultAsync(leaveType => leaveType.Id == leaveTypeId);
            
            if (leaveTypeToDelete != null)
            {
                _context.leaveTypes.Remove(leaveTypeToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
