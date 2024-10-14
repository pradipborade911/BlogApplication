using BlogApplication.Repository.impl;
using BlogApplication.Repository;
using BlogApplication.Service.impl;
using BlogApplication.Service;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register services
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<ITagRepository, TagRepository>();

            ////Register AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 39))));

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=AllPosts}");

            app.Run();
        }
    }
}
