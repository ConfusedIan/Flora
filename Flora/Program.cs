using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Flora.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FloraUserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FloraUserContext") ?? throw new InvalidOperationException("Connection string 'FloraUserContext' not found.")));

builder.Services.AddSession();

builder.Services.AddDbContext<FloraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FloraContext") ?? throw new InvalidOperationException("Connection string 'FloraContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
