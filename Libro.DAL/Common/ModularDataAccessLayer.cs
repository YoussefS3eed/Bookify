using Libro.DAL.Repo.Implementation;


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
