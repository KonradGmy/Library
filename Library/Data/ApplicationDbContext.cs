using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>().HasData(
              new BookModel(1, "Dziady cz. IV", "Adam Mickiewicz", new DateTime(1823, 6, 1), "Czwarta część dziadów"),
              new BookModel(2, "Zbrodnia i kara", "Fiodor Dostojewski", new DateTime(1867, 4, 12), "Rosyjska powieść autorstwa Fiodora Dostojewskiego napisana w 1866")
              );
        }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<BookModel> BookModel { get; set; }
        public DbSet<ReservationModel> ReservationModel { get; set; }
    }
}
