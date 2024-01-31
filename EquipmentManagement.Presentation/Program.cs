#region Usings

using EquipmentManagement.Application;
using EquipmentManagement.Infrastructure.ApplicationDbContext;
using EquipmentManagement.IoC;
using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Presentation;

#endregion

public class Program
{
    public static void Main(string[] args)
    {
        #region Services

        var builder = WebApplication.CreateBuilder(args);

        #region Register Services In IoC Layer

        DependencyContainer.ConfigureDependencies(builder.Services);

        #endregion

        #region MVC

        builder.Services.AddControllers();
        {
            builder.Services.RegisterApplicationServices();
        }

        builder.Services.AddControllersWithViews();

        #endregion

        #region Add DBContext

        builder.Services.AddDbContext<EquipmentManagementDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("EquipmentManagementDbContextConnection"));
        });

        #endregion

        #endregion

        #region Middlewares

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        #endregion
    }
}
