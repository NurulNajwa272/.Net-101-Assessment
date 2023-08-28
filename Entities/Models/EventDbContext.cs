using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }

        public DbSet<Event>? Events { get; set; }
        public DbSet<Customer>? Customers { get; set; }
    }
}
