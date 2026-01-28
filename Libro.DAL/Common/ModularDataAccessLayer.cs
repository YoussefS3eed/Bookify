using Libro.DAL.Repositories.Abstraction;
using Libro.DAL.Repositories.Implementation;
namespace Libro.DAL.Common
{
    public static class ModularDataAccessLayer
    {
        public static IServiceCollection AddDataAccessLayerInPL(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            return services;
        }
    }
}
