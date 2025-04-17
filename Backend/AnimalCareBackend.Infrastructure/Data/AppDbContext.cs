using AnimalCareBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCareBackend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
            

        public DbSet<Care> Cares { get; set; }
        public DbSet<AnimalCare> AnimalCares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<AnimalCare>()
                .HasKey(ac => new { ac.AnimalId, ac.CareId });


            modelBuilder.Entity<AnimalCare>()
                .HasOne(ac => ac.Animal)
                .WithMany(a => a.AnimalCares)
                .HasForeignKey(ac => ac.AnimalId);

            modelBuilder.Entity<AnimalCare>()
                .HasOne(ac => ac.Care)
                .WithMany(c => c.AnimalCares)
                .HasForeignKey(ac => ac.CareId);
        }
    }
}
