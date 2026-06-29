using Bookify.BLL.Dtos.User;

namespace Bookify.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()

        {
            // Category Mappings
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, SelectListItemDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));


            // Author Mappings
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Author, SelectListItemDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));



            // Book Mappings
            CreateMap<BookCreateDto, Book>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .AfterMap((dto, book) =>
                {
                    foreach (var categoryId in dto.CategoryIds)
                    {
                        book.AddCategory(categoryId!.Value);
                    }
                });

            CreateMap<BookUpdateDto, Book>();

            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : string.Empty))
            .ForMember(dest => dest.CategoryIds,
                opt => opt.MapFrom(src =>
                    src.Categories.Select(c => c.CategoryId).ToList()))
            .ForMember(dest => dest.CategoryNames,
                opt => opt.MapFrom(src =>
                    src.Categories
                        .Where(c => c.Category != null)
                        .Select(c => c.Category!.Name)
                        .ToList()));

            CreateMap<BookCopy, BookCopyDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book!.Title));


            CreateMap<BookCopyUpdateDto, BookCopy>();

            // Users
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
