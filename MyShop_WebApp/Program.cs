using Microsoft.EntityFrameworkCore;
using MyShop_WebApp.DBContext;
using MyShop_WebApp.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var Provider = builder.Services.BuildServiceProvider();
var Configuration = Provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<ShopBridgeDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));

builder.Services.AddScoped<InventoryRepository>();
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
    pattern: "{controller=Inventory}/{action=GetInventorys}/{id?}");

app.Run();
