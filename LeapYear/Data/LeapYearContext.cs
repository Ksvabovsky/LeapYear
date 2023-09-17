using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeapYear.Models;

namespace LeapYear.Data
{
    public class LeapYearContext : IdentityDbContext
    {
        public LeapYearContext(DbContextOptions<LeapYearContext> options)
            : base(options)
        {
        }
        public DbSet<LeapYearPerson> LeapYearPerson { get; set; }
    }
}