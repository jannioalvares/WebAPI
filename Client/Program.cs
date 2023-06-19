using Client.Repositories;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(GeneralRepository<,>));
            builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseStatusCodePages(async context => {
                var response = context.HttpContext.Response;

                if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
                {
                    response.Redirect("/unauthorized");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
                {
                    response.Redirect("/notfound");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
                {
                    response.Redirect("/forbidden");
                }
            });

            app.UseSession();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");

                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }

                await next();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Logins}/{id?}");

            app.Run();
        }
    }
}