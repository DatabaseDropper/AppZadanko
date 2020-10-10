using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace app1
{
    public class Context : DbContext
    {
        public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => { builder.AddConsole(); });

        public Context()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("fileName=test.db");
            optionsBuilder.UseLoggerFactory(LoggerFactory);
            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}
