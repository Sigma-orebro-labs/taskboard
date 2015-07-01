using TaskBoard.Web.Entities;
using Microsoft.Data.Entity;

namespace TaskBoard.Web.Infrastructure
{
    public class BoardContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .ForSqlServer()
                .Table("Boards");

            modelBuilder.Entity<Board>()
                .Property(x => x.Id).ForSqlServer().UseIdentity();

            modelBuilder.Entity<Issue>()
                .ForSqlServer()
                .Table("Issues");

            modelBuilder.Entity<Issue>()
                .Property(x => x.Id).ForSqlServer().UseIdentity();

            modelBuilder.Entity<IssueState>()
                .ForSqlServer()
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
