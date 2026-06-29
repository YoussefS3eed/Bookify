namespace Bookify.BLL.Service.Implementation
{
    public class AuthorService : IAuthorService, IUniqueNameValidator
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;
        public AuthorService(IAuthorRepo authorRepo, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<AuthorDto>> CreateAsync(CreateAuthorDto dto)
        {
            if (dto == null)
                return new Error("Author.InvalidData", "Author creation data cannot be null.", HttpStatusCode.BadRequest);

            if (await NameExistsAsync(dto.Name))
                return new Error("Author.NameExists", $"Author with name '{dto.Name}' already exists.", HttpStatusCode.Conflict);

            var author = _mapper.Map<Author>(dto);
            var result = await _authorRepo.AddAsync(author);

            if (result == null)
                return new Error("Author.CreationFailed", $"Failed to save Author '{dto.Name}' in the database.", HttpStatusCode.InternalServerError);

            return _mapper.Map<AuthorDto>(result);
        }
        public async Task<Result<AuthorDto>> UpdateAsync(UpdateAuthorDto dto)
        {
            if (dto == null)
                return new Error("Author.InvalidData", "Author update data cannot be null.", HttpStatusCode.BadRequest);

            var existingAuthor = await _authorRepo.GetByIdAsync(dto.Id);
            if (existingAuthor == null)
                return new Error("Author.NotFound", $"Author with ID {dto.Id} was not found.", HttpStatusCode.NotFound);

            if (existingAuthor.Name != dto.Name && await NameExistsAsync(dto.Name))
                return new Error("Author.NameExists", $"Author with name '{dto.Name}' already exists.", HttpStatusCode.Conflict);

            var author = _mapper.Map<Author>(dto);
            var result = await _authorRepo.UpdateAsync(author);

            if (result == null)
                return new Error("Author.UpdateFailed", $"Failed to update Author '{dto.Name}' in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<AuthorDto>(result);
        }
        public async Task<Result<AuthorDto>> ToggleStatusAsync(int authorId)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);
            if (author == null)
                return new Error("Author.NotFound", $"Author with ID {authorId} was not found.", HttpStatusCode.NotFound);

            var result = await _authorRepo.ToggleStatusAsync(author.Id);
            if (result == null)
                return new Error("Author.ToggleStatusFailed", $"Failed to toggle status for Author with ID {authorId} in database.", HttpStatusCode.BadRequest);

            return _mapper.Map<AuthorDto>(result);
        }
        public async Task<Result<AuthorDto>> GetByIdAsync(int authorId)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);
            if (author == null)
                return new Error("Author.NotFound", $"Author with ID {authorId} was not found.", HttpStatusCode.NotFound);

            return _mapper.Map<AuthorDto>(author);
        }
        public async Task<Result<IEnumerable<AuthorDto>>> GetAllAsync()
        {
            var authors = await _authorRepo.GetAllAsync();
            return Result.Success(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }
        public async Task<Result<IEnumerable<AuthorDto>>> GetAllNotActiveAsync()
        {
            var authors = await _authorRepo.GetAllAsync(a => a.IsDeleted);
            return Result.Success(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }
        public async Task<bool> NameExistsAsync(string name) =>
            await _authorRepo.AnyAsync(a => a.Name == name);
        public async Task<bool> IsAllowed(int Id, string name)
        {
            var category = await _authorRepo.GetSingleOrDefaultAsync(c => c.Name == name);
            return category is null || category.Id.Equals(Id);
        }
        private string GetCurrentUser()
        {
            // TODO: Implement this to return the current user for authors
            // يجب تنفيذ هذا ليعيد المستخدم الحالي
            // مثال: يمكن استخدام IHttpContextAccessor
            return "System"; // مؤقت
        }
    }
}