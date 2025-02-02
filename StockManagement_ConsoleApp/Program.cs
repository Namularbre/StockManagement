using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance.Context;
using Microsoft.Extensions.Configuration;
using StockManagement_ConsoleApp.App;
using Microsoft.Extensions.Logging;
using StockManagement_Metier.ConsoleServices;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddDebug();

        //TODO: change the minimum level if we are on production env
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Configuration of DbContext
        services.AddDbContext<StockManagementContext>(options =>
        {
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("StockManagement")
            );
        });

        // Services dependancy injection for the application class
        services.AddScoped<IApp, App>();

        // Other services
        services.AddScoped<IConsoleProductService, ConsoleProductService>();
        services.AddScoped<IConsoleAlertService, ConsoleAlertService>();
    });

var host = builder.Build();

// run the app
var app = host.Services.GetRequiredService<IApp>();
await app.Run();
