using Bookify.BLL.Mapper;

namespace Bookify.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<IUniqueNameValidator, CategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<AuthorService>();
            services.AddScoped<IUniqueNameValidator, AuthorService>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IBookCopyService, BookCopyService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(x => x.AddProfile<DomainProfile>());
            return services;
        }
    }
}
