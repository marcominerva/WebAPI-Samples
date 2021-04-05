using System.Reflection;
using CalendarApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarApi.DataAccessLayer
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
