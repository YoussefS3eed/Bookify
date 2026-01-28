using AutoMapper;
using Libro.BLL.DTOs.Author;
using Libro.BLL.DTOs.Category;
using Libro.PL.ViewModels.Author;
using Libro.PL.ViewModels.Category;

namespace Libro.PL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryDto, CategoryFormViewModel>();
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryFormViewModel, CategoryCreateDto>();
            CreateMap<CategoryFormViewModel, CategoryUpdateDto>();

            CreateMap<AuthorDto, AuthorFormViewModel>();
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorFormViewModel, AuthorCreateDto>();
            CreateMap<AuthorFormViewModel, AuthorUpdateDto>();
        }
    }
}
