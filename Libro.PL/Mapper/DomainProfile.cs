using Libro.BLL.DTOs;
using Libro.BLL.DTOs.Author;
using Libro.BLL.DTOs.Book;
using Libro.BLL.DTOs.BookCopy;
using Libro.BLL.DTOs.Category;
using Libro.PL.ViewModels.Author;
using Libro.PL.ViewModels.Book;
using Libro.PL.ViewModels.BookCopy;
using Libro.PL.ViewModels.Category;

namespace Libro.PL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryDTO, CategoryFormViewModel>();
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CategoryFormViewModel, CreateCategoryDTO>();
            CreateMap<CategoryFormViewModel, UpdateCategoryDTO>();

            CreateMap<AuthorDTO, AuthorFormViewModel>();
            CreateMap<AuthorDTO, AuthorViewModel>();
            CreateMap<AuthorFormViewModel, CreateAuthorDTO>();
            CreateMap<AuthorFormViewModel, UpdateAuthorDTO>();


            // Map from BLL DTOs to ViewModels
            CreateMap<BookDTO, BookFormViewModel>()
                .ForMember(dest => dest.SelectedCategories, opt => opt.MapFrom(src => src.CategoryIds))
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore());


            // Map from ViewModels to BLL DTOs
            CreateMap<BookFormViewModel, CreateBookDTO>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.SelectedCategories))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => "System from mapper"));

            CreateMap<BookFormViewModel, UpdateBookDTO>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.SelectedCategories))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(_ => "System System from mapper"));

            CreateMap<SelectListItemDTO, SelectListItem>();

            CreateMap<BookDTO, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorName))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryNames));

            CreateMap<BookCopyDTO, BookCopyViewModel>();

            CreateMap<BookCopyFormViewModel, CreateBookCopyDTO>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => "System from mapper"));

            CreateMap<CreateBookCopyDTO, BookCopyFormViewModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //BookCopyDTO to  BookCopyFormViewModel
            CreateMap<BookCopyDTO, BookCopyFormViewModel>()
                .ForMember(dest => dest.ShowRentalInput, opt => opt.MapFrom(src => src.Book.IsAvailableForRental));

            // BookCopyFormViewModel to UpdateBookCopyDTO
            CreateMap<BookCopyFormViewModel, UpdateBookCopyDTO>();

            // BookCopyDTO to BookCopyViewModel
        }
    }
}
