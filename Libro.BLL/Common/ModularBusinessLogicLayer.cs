global using Libro.BLL.Mapper;

namespace Libro.BLL.Common
{
    public static class ModularBusinessLogicLayer
    {
        public static IServiceCollection AddBusinessLogicLayerInPL(this IServiceCollection services)
        {
           
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(x => x.AddProfile<DomainProfile>());
            return services;
        }
    }
}
