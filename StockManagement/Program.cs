using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance.Context;
using StockManagement_Metier.Services;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<StockManagementContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<StockManagementContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IStorageService, StorageService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IAlertService, AlertService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        var app = builder.Build();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        string? pathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATHBASE");

        if (!pathBase.IsNullOrEmpty())
        {
            logger.LogInformation("Using path base: {PathBase}", pathBase);
            app.UsePathBase(pathBase);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        app.Run();
    }
}