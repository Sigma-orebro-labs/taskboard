using System.Data.Entity;
using TaskBoard.Domain.Entities;
using TaskBoard.Web.Infrastructure.Extensions;

namespace TaskBoard.Persistence
{
    public class BoardContext : DbContext
    {
        public BoardContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntity<Board>();
            modelBuilder.RegisterEntity<Issue>();
            modelBuilder.RegisterEntity<IssueState>();

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
