using BusinessPortal2.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2.Data
{
    public class PersonaldataContext : DbContext
    {

        public PersonaldataContext(DbContextOptions<PersonaldataContext> options) : base(options)
        {

        }

        public DbSet<Personal> personals {get; set;}
        public DbSet<LeaveType> leaveTypes {get; set;}


    }
}
