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
        public async Task<Response<AuthorDTO>> CreateAsync(CreateAuthorDTO dto)
        {
            if (dto == null)
                return new(null, "Invalid data.", true, HttpStatusCode.BadRequest);

            if (await NameExistsAsync(dto.Name))
                return new(null, "Author name already exists.", true, HttpStatusCode.Conflict);

            var author = _mapper.Map<Author>(dto);
            var result = await _authorRepo.AddAsync(author);

            if (result == null)
                return new(null, "Failed to create author in database.", true, HttpStatusCode.InternalServerError);

            return new(_mapper.Map<AuthorDTO>(result), null, false);
        }
        public async Task<Response<AuthorDTO>> UpdateAsync(UpdateAuthorDTO dto)
        {
            if (dto == null)
                return new(null, "Invalid data.", true, HttpStatusCode.BadRequest);

            var existingAuthor = await _authorRepo.GetByIdAsync(dto.Id);
            if (existingAuthor == null)
                return new(null, "Author not found.", true, HttpStatusCode.NotFound);

            if (existingAuthor.Name != dto.Name && await NameExistsAsync(dto.Name))
                return new(null, "Author name already exists.", true, HttpStatusCode.Conflict);

            var author = _mapper.Map<Author>(dto);
            var result = await _authorRepo.UpdateAsync(author);

            if (result == null)
                return new(null, "Database error.", true, HttpStatusCode.BadRequest);

            return new(_mapper.Map<AuthorDTO>(result), null, false);
        }
        public async Task<Response<AuthorDTO>> ToggleStatusAsync(int authorId)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);
            if (author == null)
                return new(null, "Author not found.", true, HttpStatusCode.NotFound);

            var result = await _authorRepo.ToggleStatusAsync(author.Id);
            if (result == null)
                return new(null, "Database error.", true, HttpStatusCode.BadRequest);

            return new(_mapper.Map<AuthorDTO>(result), null, false);
        }
        public async Task<Response<AuthorDTO>> GetByIdAsync(int authorId)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);
            if (author == null)
                return new(null, "Author not found.", true, HttpStatusCode.NotFound);

            return new(_mapper.Map<AuthorDTO>(author), null, false);
        }
        public async Task<Response<IEnumerable<AuthorDTO>>> GetAllAsync()
        {
            var authors = await _authorRepo.GetAllAsync();
            return new(_mapper.Map<IEnumerable<AuthorDTO>>(authors), null, false);
        }
        public async Task<Response<IEnumerable<AuthorDTO>>> GetAllNotActiveAsync()
        {
            var authors = await _authorRepo.GetAllAsync(a => a.IsDeleted);
            return new(_mapper.Map<IEnumerable<AuthorDTO>>(authors), null, false);
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