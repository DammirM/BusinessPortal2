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

        public async Task<LeaveType> CreateLeave(LeaveType leaveType)
        {
            var register = await _context.leaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();
            return register.Entity;
        }

        public async Task<IEnumerable<LeaveType>> GetAll()
        {
            return await _context.leaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetById(int id)
        {
            var identity = await _context.leaveTypes.FirstOrDefaultAsync(x => x.PersonalId == id);
            if (identity != null)
            {
                return identity;
            }
            return null;
        }

        public async Task<LeaveType> UpdateLeave(int id, LeaveType tr)
        {
            var identified = await _context.leaveTypes.FirstOrDefaultAsync(e => e.PersonalId == tr.PersonalId);
            if (identified != null)
            {
                identified.Sick = tr.Sick;
                identified.Vacation = tr.Vacation;
                identified.Vabb = tr.Vabb;
                await _context.SaveChangesAsync();
                return identified;
            }
            return null;
        }

        public async Task DeleteLeave(int id)
        {
            var leaveTypeToDelete = await _context.leaveTypes
                .FirstOrDefaultAsync(leaveType => leaveType.PersonalId == id);
            _context.leaveTypes.Remove(leaveTypeToDelete);
        }
    }
}
