using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebAPI.Context;
using WebAPI.Model;
using WebAPI.Utility;

namespace WebAPI.Context
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role
            {
                Guid = Guid.Parse("a4c1b16c-9753-4d01-7804-08db60296455"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("5e735041-ce30-43b9-d7aa-08db60bf349a"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("74cd5c21-af22-4d46-7581-08db58444df6"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });


            builder.Entity<Employee>().HasIndex(e => new{ 
                e.Nik, 
                e.Email, 
                e.PhoneNumber
            }).IsUnique();

            //Relation between Education and University(1 to Many)
            builder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            //Relation between Education and Employee(1 to 1)
            builder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);

            builder.Entity<Booking>()
                .HasOne(e => e.Employee)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.EmployeeGuid);

            builder.Entity<Booking>()
                .HasOne(r => r.Room)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.RoomGuid);

            builder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(ar => ar.AccountGuid);

            builder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRole)
                .HasForeignKey(ar => ar.RoleGuid);

            builder.Entity<Account>()
                .HasOne(e => e.Employee)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(a => a.Guid);              
        }
    }
}
