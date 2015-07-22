using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Web.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
         public static void RegisterEntity<TEntity>(this DbModelBuilder modelBuilder) where TEntity : Entity
         {
             modelBuilder.Entity<TEntity>()
                 .Property(x => x.Id)
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

             var tableName = typeof(TEntity).Name + "s";

             modelBuilder.Entity<TEntity>()
                 .ToTable(tableName);
         }
    }
}