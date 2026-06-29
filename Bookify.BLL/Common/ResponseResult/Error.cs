namespace Bookify.BLL.Common.ResponseResult
{
    public record Error(string Code, string Message, HttpStatusCode StatusCode = HttpStatusCode.BadRequest)
    {
        public static readonly Error None = new(string.Empty, string.Empty, HttpStatusCode.OK);
        public static readonly Error NullValue = new("Error.NullValue", "The value cannot be null.", HttpStatusCode.BadRequest);
    }
}
