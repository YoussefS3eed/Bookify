using Libro.DAL.Repo.Implementation;


namespace Libro.DAL.Common
{
    public static class ModularDataAccessLayer
    {
        public static IServiceCollection AddDataAccessLayerInPL(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            return services;
        }
    }
}
