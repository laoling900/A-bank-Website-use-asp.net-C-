using Microsoft.EntityFrameworkCore;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.BackgroudServices;

var consoleLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<BankAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(BankAppContext)));
    options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<CustomerOps>();
builder.Services.AddScoped<AccountOps>();
builder.Services.AddScoped<BillPayOps>();
builder.Services.AddHostedService<BillPayBackgroundService>();

builder.Services.AddSession(option =>
{
    option.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedDataFromJson.Initialize(services);

    }catch (Exception ex)
    {
        var logger=services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");

    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
