using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.Services;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;

namespace OnlineShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add MVC

            builder.Services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            #endregion

            #region Contexts

            builder.Services.AddDbContext<RoyaContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RoyaShopConnection"));
            });

            #endregion

            #region IoC

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
            builder.Services.AddTransient<IPermissionService, PermissionService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            #endregion

            #region Authentication

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                });

            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
                name: "areas",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.Run();
        }
    }
}
