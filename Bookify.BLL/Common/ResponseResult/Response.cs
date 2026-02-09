namespace Bookify.BLL.Common.ResponseResult
{
    public record Response<T>(T? Result, string? ErrorMessage, bool HasErrorMessage, HttpStatusCode StatusCode = HttpStatusCode.OK);
}
