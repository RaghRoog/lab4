using Microsoft.EntityFrameworkCore;

namespace lab4 {
    public class DatabaseContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RegisteredEvent> RegisteredEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data source=C:\\Users\\szumo\\DataGripProjects\\test3\\identifier.sqlite");
        }
    }
}
