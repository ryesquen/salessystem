namespace SalesSystem.Application.Commons.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            this.IsSuccess = true;
        }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}