using TaskBoard.Web.Entities;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;

namespace TaskBoard.Web.Infrastructure
{
    public class BoardContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            // The development connection string is defined in the config.json file.
            // The connection strings for the Azure web app(s) are defined in the web app
            // settings in the Azure portal
            optionsBuilder.UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .ForRelational()
                .Table("Boards");

            modelBuilder.Entity<Board>()
                .Property(x => x.Id).ForSqlServer().UseIdentity();

            modelBuilder.Entity<Issue>()
                .ForRelational()
                .Table("Issues");

            modelBuilder.Entity<Issue>()
                .Property(x => x.Id).ForSqlServer().UseIdentity();

            modelBuilder.Entity<IssueState>()
                .ForRelational()
                .Table("IssueStates");

            modelBuilder.Entity<IssueState>()
                .Property(x => x.Id).ForSqlServer().UseIdentity();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Board> Boards
        {
            get { return Set<Board>(); }
        }

        public DbSet<Issue> Issues
        {
            get { return Set<Issue>(); }
        }

        public DbSet<IssueState> IssueStates
        {
            get { return Set<IssueState>(); }
        }
    }
}
