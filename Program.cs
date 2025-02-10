using Microsoft.EntityFrameworkCore;
using ecs_fargate.DbContexts;
using Microsoft.AspNetCore.DataProtection;

namespace ecs_fargate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HomeDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            //        builder.Services.AddDataProtection()
            //.PersistKeysToFileSystem(new DirectoryInfo(@"C:\keys"));

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5005); 
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
