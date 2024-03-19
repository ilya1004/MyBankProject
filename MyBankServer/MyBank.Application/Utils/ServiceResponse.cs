namespace MyBank.Application.Utils;

public class ServiceResponse <T>
{
    public ServiceResponse() { }
    public ServiceResponse(bool status, string message, T data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; } = default;
}
