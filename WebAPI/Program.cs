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

            //using generic
            builder.Services.AddScoped<IGenericRepository<Account>, GenericRepository<Account>>();
            builder.Services.AddScoped<IGenericRepository<University>, GenericRepository<University>>();
            builder.Services.AddScoped<IGenericRepository<Education>, GenericRepository<Education>>();
            builder.Services.AddScoped<IGenericRepository<Room>, GenericRepository<Room>>();
            builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            builder.Services.AddScoped<IGenericRepository<AccountRole>, GenericRepository<AccountRole>>();


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