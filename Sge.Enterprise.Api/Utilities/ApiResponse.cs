namespace Sge.Enterprise.Api.Utilities
{
    public class ApiResponse
    {
        public bool Status { get; }
        public string Message { get; }
        public object? Data { get; }

        public ApiResponse(
            object? data = null,
            bool status = true,
            string message = "Proceso exitoso")
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}