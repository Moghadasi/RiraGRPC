using Microsoft.EntityFrameworkCore;
using Rira.Models.User;

namespace Rira.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class RiraCommandContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public RiraCommandContext(DbContextOptions<RiraCommandContext> options) : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UserEntity> Users { get; set; } = default!;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().Property(m => m.NationalCode).IsUnicode(false);
            modelBuilder.Entity<UserEntity>().HasIndex(m => m.NationalCode).IsUnique(true);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
