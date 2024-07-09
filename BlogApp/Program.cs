using System.Net;
using System.Runtime.CompilerServices;
using BlogApp.Infrastructe.Extensions;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Adds controllers and views services to the service container.

builder.Services.ConfigureDbContext(builder.Configuration);
// Configures the DbContext using the application's configuration settings.

builder.Services.ConfigureIdentity();
// Configures ASP.NET Core Identity services.

builder.Services.ConfigureRepositoryRegistration();
// Configures repository registrations for dependency injection.

builder.Services.ConfigureServiceRegistration();
// Configures service registrations for dependency injection.

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();



app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
  endpoints.MapAreaControllerRoute(
      name: "Admin",
      areaName: "Admin",
      pattern: "Admin/{controller=DashBoard}/{action=Index}/{id?}"
 );
  endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
  endpoints.MapControllerRoute(
        name: "blog",
        pattern: "Post/{action=Index}/{id?}",
        defaults: new { controller = "Blog" });
  endpoints.MapControllerRoute(
name: "GetPostsByCategory",
pattern: "Blog/GetPostsByCategory/{categoryname?}", 
defaults: new { controller = "Blog", action = "GetPostsByCategory" }
);
});



app.ConfigureAndCheckMigration();
// Configure application and check database migrations

app.ConfigureDefaultAdminUser();
// Configure the default admin user





// The following code was moved above into the UseEndpoints method for proper routing setup:
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
