namespace Libro.BLL.ModelVM.ResponseResult
{
    public record Response<T>(T? Result, string? ErrorMessage, bool HasErrorMessage);
}
