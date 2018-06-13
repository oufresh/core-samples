using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace treeDb.db
{
    public partial class appContext : DbContext
    {
        public virtual DbSet<Closure> Closure { get; set; }
        public virtual DbSet<Tree> Tree { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"DataSource=app.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Closure>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Child });

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.Closure)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Tree>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
