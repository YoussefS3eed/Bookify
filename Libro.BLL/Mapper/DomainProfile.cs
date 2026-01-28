using Libro.BLL.DTOs.Author;
using Libro.BLL.DTOs.Category;

namespace Libro.BlL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorDto>();
        }
    }
}
