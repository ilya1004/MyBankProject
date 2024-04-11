namespace MyBank.API.Interfaces;

public interface ICookieValidator
{
    public (bool status, string? message, int? errorCode, string? role, int? id) HandleCookie(string cookieValue);
}
