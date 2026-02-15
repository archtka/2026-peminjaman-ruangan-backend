using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Models;

namespace SistemPeminjamanAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1, 
                    Name = "Lab Komputer Dasar",
                    Description = "Lab Praktikum Semester 1",
                    Capacity = 30,
                    IsAvailable = true
                },
                new Room
                {
                    Id = 2,
                    Name = "Aula Serbaguna",
                    Description = "Untuk seminar dan rapat umum",
                    Capacity = 100,
                    IsAvailable = true
                }
            );
        }
    }
}