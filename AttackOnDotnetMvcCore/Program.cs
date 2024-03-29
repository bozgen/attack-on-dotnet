using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AttackOnDotnetMvcCore.Data;
using Microsoft.AspNetCore.Identity;
using AttackOnDotnetMvcCore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AttackOnDotnetMvcCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AttackOnDotnetMvcCoreContext") ?? throw new InvalidOperationException("Connection string 'AttackOnDotnetMvcCoreContext' not found.")));

/*builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AttackOnDotnetMvcCoreContext>();*/

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AttackOnDotnetMvcCoreContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
