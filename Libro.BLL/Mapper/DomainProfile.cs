using Libro.BLL.ModelVM.Author;

namespace Libro.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CategoryFormVM>().ReverseMap();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorFormVM>().ReverseMap();
        }
    }
}
