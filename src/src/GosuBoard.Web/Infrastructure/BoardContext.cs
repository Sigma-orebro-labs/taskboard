﻿using GosuBoard.Web.Entities;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;

namespace GosuBoard.Web.Infrastructure
{
    public class BoardContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new Configuration().AddJsonFile("config.json");

            var connectionString = config.Get("connectionString");

            optionsBuilder.UseSqlServer(connectionString);

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
    }
}
