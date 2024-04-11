namespace MyBank.API.DataTransferObjects;

public class ErrorDto
{
    public ErrorDto() { }

    public ErrorDto(string controllerName, string message)
    {
        ControllerName = controllerName;
        Message = message;
    }

    public string ControllerName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
