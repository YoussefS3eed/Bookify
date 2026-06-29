using Bookify.BLL;
using Bookify.DAL;
using Bookify.PL.Mapper;
using Bookify.PL.Settings;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

namespace Bookify.PL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // ------------------- Add Connection String and DbContext -------------------
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookifyDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // ------------------- Identity -------------------
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BookifyDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // ------------------- Dependency Registration -------------------
            services.AddDataAccessLayer();
            services.AddBusinessLogicLayer();
            services.AddAutoMapper(x => x.AddProfile<DomainProfile>());

            // ------------------- MVC & Add-ons -------------------
            services.AddControllersWithViews();
            services.Configure<CloudinarySettings>(configuration.GetSection(nameof(CloudinarySettings)));
            services.AddExpressiveAnnotations();

            return services;
        }
    }
}
