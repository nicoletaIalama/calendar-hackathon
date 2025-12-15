using Calendar.Api.Domain.DashboardModels;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Domain.DashboardModels
{
    public class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<DashboardHoliday> Holidays { get; set; }
    }
}
