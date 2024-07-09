using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

namespace BlogApp.Infrastructe.Extensions
{
    public static class ServiceExtension
    {
        // Configures the DbContext to use SQLite with the specified connection string retrieved from configuration.
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("sqlconnection"),
                    b => b.MigrationsAssembly("BlogApp"));

                options.EnableSensitiveDataLogging(true);

            });

        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
                // Registers repository services with scoped lifetime.
                services.AddScoped<IRepositoryManager, RepositoryManager>();
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<ICommentRepository , CommentRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            // Registers service manager and related services with scoped lifetime.
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IPostServices, PostManager>();
            services.AddScoped<ICategoryServices, CategoryManager>();
            services.AddScoped<IAuthService,AuthManager>();
            services.AddScoped<ICommentServices, CommentManager>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<RepositoryContext>();
        }
    }
}