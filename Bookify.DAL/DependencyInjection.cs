using Bookify.DAL.Repositories.Implementation;

namespace Bookify.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IBookRepo, BookRepo>();
            services.AddScoped<IBookCopyRepo, BookCopyRepo>();
            return services;
        }
    }
}
