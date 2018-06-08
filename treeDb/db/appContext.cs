using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace treeDb.models
{
    public partial class appContext : DbContext
    {
        public virtual DbSet<Closure> Closure { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Tree> Tree { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite(@"DataSource=app.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Closure>(entity =>
            {
                entity.HasKey(e => e.Parent);

                entity.Property(e => e.Parent).ValueGeneratedNever();

                entity.HasOne(d => d.ParentNavigation)
                    .WithOne(p => p.Closure)
                    .HasForeignKey<Closure>(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("NUMERIC")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Tree>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
