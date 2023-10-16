using BusinessPortal2.Data;
using BusinessPortal2.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Services
{
    public class LeaveTypeRepo : ILeaveTypeRepository
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

        public async Task<LeaveType> DeleteLeave(int personalId)
        {
            var TtoDel = await _context.leaveTypes.FirstOrDefaultAsync(x => x.PersonalId == personalId);
            if (TtoDel != null)
            {
                _context.leaveTypes.Remove(TtoDel);
                await _context.SaveChangesAsync();
                return TtoDel;
            }
            return null;
        }

        public async Task<IEnumerable<LeaveType>> GetAll()
        {
            return await _context.leaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetById(int id)
        {
            var identi = await _context.leaveTypes.FirstOrDefaultAsync(x => x.PersonalId == id);
            if (identi != null)
            {
                return identi;
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
    }
}
