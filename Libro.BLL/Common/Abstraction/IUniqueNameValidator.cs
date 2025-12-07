namespace Libro.BLL.Common.Abstraction
{
    public interface IUniqueNameValidator
    {
        Task<bool> NameExistsAsync(string name);
    }
}
