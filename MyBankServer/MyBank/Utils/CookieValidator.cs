using MyBank.API.Interfaces;
using Newtonsoft.Json.Linq;

namespace MyBank.API.Utils;

public class CookieValidator : ICookieValidator
{
    public (bool status, string? message, int? errorCode, string? role, int? id) HandleCookie(
        string cookieValue
    )
    {
        int startIndex = cookieValue.IndexOf('=');
        var token = cookieValue[(startIndex + 1)..];

        var jwtParts = token.Split('.');

        if (jwtParts.Length != 3)
        {
            return (false, "Неверный формат JWT-токена.", 2, null, null);
        }

        var base64Payload = jwtParts[1];
        var payloadBytes = Convert.FromBase64String(base64Payload);
        var payloadJson = System.Text.Encoding.UTF8.GetString(payloadBytes);

        var payloadData = JObject.Parse(payloadJson);

        var expiration = Convert.ToInt64(payloadData["exp"]?.ToString());

        var expirationDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(
            expiration
        );

        if (expirationDateTime < DateTime.UtcNow)
        {
            return (false, "Срок валидности cookie уже истек.", 3, null, null);
        }

        var role = payloadData["Role"]?.ToString();
        int id;

        if (role == "User")
        {
            id = Convert.ToInt32(payloadData["userId"]?.ToString());
        }
        else if (role == "Moderator")
        {
            id = Convert.ToInt32(payloadData["moderatorId"]?.ToString());
        }
        else if (role == "Admin")
        {
            id = Convert.ToInt32(payloadData["adminId"]?.ToString());
        }
        else
        {
            return (false, "Значение role не найдено.", 4, null, null);
        }

        return (true, null, null, role, id);
    }
}
