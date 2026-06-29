using Bookify.BLL.Dtos;
using Bookify.BLL.Dtos.Author;
using Bookify.BLL.Dtos.Book;
using Bookify.BLL.Dtos.BookCopy;
using Bookify.BLL.Dtos.Category;
using Bookify.BLL.Dtos.User;
using Bookify.PL.ViewModels.Author;
using Bookify.PL.ViewModels.Book;
using Bookify.PL.ViewModels.BookCopy;
using Bookify.PL.ViewModels.Category;
using Bookify.PL.ViewModels.User;

namespace Bookify.PL.Mapper
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
            CreateMap<AuthorFormViewModel, CreateAuthorDto>();
            CreateMap<AuthorFormViewModel, UpdateAuthorDto>();


            // Map from BLL Dtos to ViewModels
            CreateMap<BookDto, BookFormViewModel>()
                .ForMember(dest => dest.SelectedCategories, opt => opt.MapFrom(src => src.CategoryIds))
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore());


            // Map from ViewModels to BLL Dtos
            CreateMap<BookFormViewModel, BookCreateDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.SelectedCategories))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => "System from mapper"));

            CreateMap<BookFormViewModel, BookUpdateDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.SelectedCategories))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(_ => "System System from mapper"));

            CreateMap<SelectListItemDto, SelectListItem>();

            CreateMap<BookDto, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorName))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryNames));

            CreateMap<BookCopyDto, BookCopyViewModel>();

            CreateMap<BookCopyFormViewModel, BookCopyCreateDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => "System from mapper"));

            CreateMap<BookCopyCreateDto, BookCopyFormViewModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //BookCopyDto to  BookCopyFormViewModel
            CreateMap<BookCopyDto, BookCopyFormViewModel>()
                .ForMember(dest => dest.ShowRentalInput, opt => opt.MapFrom(src => src.Book.IsAvailableForRental));

            // BookCopyFormViewModel to UpdateBookCopyDto
            CreateMap<BookCopyFormViewModel, BookCopyUpdateDto>();

            // UserDto to UserViewModel
            CreateMap<UserDto, UserViewModel>();

            CreateMap<UserFormViewModel, UserCreateDto>();

            CreateMap<UserUpdateDto, UserFormViewModel>().ReverseMap();

            CreateMap<UserResetPasswordDto, ResetPasswordFormViewModel>().ReverseMap();

        }
    }
}
