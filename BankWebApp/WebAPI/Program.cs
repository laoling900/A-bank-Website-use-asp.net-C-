using Microsoft.EntityFrameworkCore;
using BankWebAdmin.Data;
using BankWebAdmin.Models.DataManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BankAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankAppContext")));

builder.Services.AddScoped<CustomerManager>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<AccountManager>();
builder.Services.AddScoped<PayeeManager>();
builder.Services.AddScoped<BillPayManager>();
builder.Services.AddScoped<TransactionManager>();

//builder.Services.AddTransient<MovieManager>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// .NET 6 Minimal APIs - Simple Example.
// See here for more information:
// https://docs.microsoft.com/en-au/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0

// GET api/UsingMapGet?name=Matthew&repeat=100
app.MapGet("api/UsingMapGet", (string name, int? repeat) =>
{
    if (string.IsNullOrWhiteSpace(name))
        name = "(empty)";
    if (repeat is null or < 1)
        repeat = 1;

    return string.Join(' ', Enumerable.Repeat(name, repeat.Value));
});

app.Run();