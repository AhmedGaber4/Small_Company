using Data;
using Logic.Interfaces;
using Logic.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PL.mapper;

namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyDbcontexts>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("df"));
            });
            builder.Services.AddScoped<IDepRepositories, DepRepositories>();
            builder.Services.AddScoped<IEmpRepositories, EmpRepositories>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(HolaMapper));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => 
                {
                    o.LoginPath= new PathString("/Account/Login");
                    o.AccessDeniedPath = new PathString("/Home/Error");
                });
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric= true;
                    options.Password.RequiredLength = 6;
                    options.SignIn.RequireConfirmedAccount= false;
                   // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

                }).AddEntityFrameworkStores<MyDbcontexts>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


            var app = builder.Build();
         
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())   
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignUp}/{id?}");

            app.Run();
        }
    }
}