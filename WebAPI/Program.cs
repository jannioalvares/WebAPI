using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

            builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));

            /*builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();*/

            //using generic
            builder.Services.AddScoped<IRepository<Account>, GenericRepository<Account>>();
            builder.Services.AddScoped<IRepository<University>, GenericRepository<University>>();
            builder.Services.AddScoped<IRepository<Education>, GenericRepository<Education>>();
            builder.Services.AddScoped<IRepository<Room>, GenericRepository<Room>>();
            builder.Services.AddScoped<IRepository<Role>, GenericRepository<Role>>();
            builder.Services.AddScoped<IRepository<Employee>, GenericRepository<Employee>>();
            builder.Services.AddScoped<IRepository<AccountRole>, GenericRepository<AccountRole>>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}