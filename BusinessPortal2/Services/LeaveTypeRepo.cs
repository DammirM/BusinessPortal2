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
                var register = await _context.LeaveType.AddAsync(leaveType);
                await _context.SaveChangesAsync();
                return register.Entity;
            }
            return null;
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveType()
        {
            var leaveTypeAll = await _context.LeaveType.Include(leaveType => leaveType.leaveRequests).ToListAsync();

            return leaveTypeAll;
        }

        public async Task<LeaveType> GetLeaveTypeById(int leaveTypeId)
        {
            var leaveTypeById = await _context.LeaveType.FirstOrDefaultAsync(leaveType => leaveType.Id == leaveTypeId);

            return leaveTypeById;
        }

        public async Task<LeaveType> UpdateLeaveType(LeaveType leaveType)
        {
            if (leaveType != null)
            {
                _context.LeaveType.Update(leaveType);
                await _context.SaveChangesAsync();
                return leaveType;
            }
            return null;
        }

        

        public async Task<bool> DeleteLeaveType(int leaveTypeId)
        {
            var leaveTypeToDelete = await _context.LeaveType
                .FirstOrDefaultAsync(leaveType => leaveType.Id == leaveTypeId);

            if (leaveTypeToDelete != null)
            {
                _context.LeaveType.Remove(leaveTypeToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        

        public async Task<IEnumerable<LeaveType>> GetAllLeaveTypeByPersonalId(int personalId)
        {
            var leaveTypeById = await _context.LeaveType.Where(l => l.PersonalId == personalId).ToListAsync();

            return leaveTypeById;
        }

       

        public async Task<LeaveType> UpdateLeaveByNameType(LeaveType leaveType, string name)
        {
            var leaveTypes = await _context.LeaveType.Where(l => l.LeaveName.ToLower() == name.ToLower()).ToListAsync();

            if (leaveTypes.Any())
            {
                foreach (var leaveTyped in leaveTypes)
                {
                    // Update the properties
                    leaveTyped.LeaveName = leaveType.LeaveName;
                    leaveTyped.LeaveDays = leaveType.LeaveDays;

                }

                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<bool> DeleteLeaveTypeByName(string name)
        {
            var leaveTypeToDelete = _context.LeaveType
                .Where(leaveType => leaveType.LeaveName.ToLower() == name.ToLower());

            if (leaveTypeToDelete.Any())
            {
                foreach (var leaveTyped in leaveTypeToDelete)
                {
                    _context.LeaveType.Remove(leaveTyped);

                }

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<LeaveType> CreateLeaveTypeForAll(LeaveType leaveType)
        {
            var personal = await _context.personals.ToListAsync();

            if (leaveType != null && personal.Any())
            {
                var leaveTypeExists = await _context.LeaveType
                    .AnyAsync(lt => lt.LeaveName.ToLower() == leaveType.LeaveName.ToLower());

                if (leaveTypeExists)
                {
                    return null;
                }

                foreach (var pers in personal)
                {
                    var leave = new LeaveType()
                    {
                        LeaveName = leaveType.LeaveName.ToLower(),
                        LeaveDays = leaveType.LeaveDays,
                        PersonalId = pers.Id,
                    };

                    _context.LeaveType.Add(leave);
                }

                await _context.SaveChangesAsync();
                return leaveType;
            }

            return null;
        }


        public async Task UpdateLeaveTypeOnApproved(int days, int personalId)
        {
            var personToUpdate = await _context.LeaveType.FirstOrDefaultAsync(person => person.PersonalId == personalId);
            if (personToUpdate != null)
            {
                personToUpdate.LeaveDays -= days;
                await _context.SaveChangesAsync();
            }
        }
    }
}
