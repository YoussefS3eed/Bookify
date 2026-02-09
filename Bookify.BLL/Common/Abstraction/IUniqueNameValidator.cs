namespace Bookify.BLL.Common.Abstraction
{
    public interface IUniqueNameValidator
    {
        Task<bool> NameExistsAsync(string name);
    }
}
