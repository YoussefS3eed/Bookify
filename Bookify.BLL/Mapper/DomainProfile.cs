using Bookify.BLL.DTOs;
using Bookify.BLL.DTOs.Author;
using Bookify.BLL.DTOs.Book;
using Bookify.BLL.DTOs.BookCopy;
using Bookify.BLL.DTOs.Category;
using Bookify.DAL.Entities;

namespace Bookify.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()

        {
            // Category Mappings
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, SelectListItemDTO>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));


            // Author Mappings
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<UpdateAuthorDTO, Author>();
            CreateMap<Author, AuthorDTO>();
            CreateMap<Author, SelectListItemDTO>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));



            // Book Mappings
            CreateMap<CreateBookDTO, Book>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .AfterMap((dto, book) =>
                {
                    foreach (var categoryId in dto.CategoryIds)
                    {
                        book.AddCategory(categoryId!.Value);
                    }
                });

            CreateMap<UpdateBookDTO, Book>();

            CreateMap<Book, BookDTO>()
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

            CreateMap<BookCopy, BookCopyDTO>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book!.Title));


            CreateMap<UpdateBookCopyDTO, BookCopy>();
        }
    }
}
