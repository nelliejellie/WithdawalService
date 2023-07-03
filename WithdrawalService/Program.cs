using Microsoft.EntityFrameworkCore;
using Serilog;
using WithdrawalService;
using WithdrawalService.Data;
using WithdrawalService.Domain;
using WithdrawalService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        
        IConfiguration configuration = hostContext.Configuration;

        AppSettings.ConnectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(AppSettings.ConnectionString);

        services.AddScoped<AppDbContext>(db => new AppDbContext(optionsBuilder.Options));
        services.Configure<Paystack>(configuration.GetSection("Paystack"));
        services.AddSingleton<BankHelper>();
        services.AddSingleton<UserHelper>();
        services.AddHostedService<Worker>();
    })
    .UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/errors.txt", rollingInterval: RollingInterval.Hour)
    .Enrich.FromLogContext()
    )
    .Build();

await host.RunAsync();
