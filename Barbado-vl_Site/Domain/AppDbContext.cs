using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

using Barbado_vl_Site.Domain.Entities;

namespace Barbado_vl_Site.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "9be96878-8d14-4a65-a41a-8068352b6d6a",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "d3394515-bc0a-46c4-ba06-7a51c9523b31",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "mmail@mail.com",
                NormalizedEmail = "MMAIL@MAIL.RU",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "supperpassword"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "9be96878-8d14-4a65-a41a-8068352b6d6a",
                UserId = "d3394515-bc0a-46c4-ba06-7a51c9523b31"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("18ac51c7-566b-4579-8611-6a9e7fe18405"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("e46095d1-e785-4ba9-82d5-4cc0565d3f38"),
                CodeWord = "PageServices",
                Title = "Мои Услуги"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("aa4a66f9-a027-4727-be57-fde5f2c8ca4c"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });
        }
    }
}
