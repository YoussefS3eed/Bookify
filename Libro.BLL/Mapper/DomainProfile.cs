using AutoMapper;
using Libro.BLL.ModelVM.Category;
using Libro.BLL.ModelVM.ViewModel;
using Libro.DAL.Entities;

namespace Libro.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CategoryFormVM>().ReverseMap();
        }
    }
}
