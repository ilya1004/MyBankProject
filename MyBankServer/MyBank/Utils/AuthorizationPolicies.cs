namespace MyBank.API.Utils;

public static class AuthorizationPolicies
{
    public const string UserPolicy = "UserPolicy";
    public const string ModeratorPolicy = "ModeratorPolicy";
    public const string AdminPolicy = "AdminPolicy";
    public const string UserAndModeratorPolicy = "UserAndModeratorPolicy";
    public const string ModeratorAndAdminPolicy = "ModeratorAndAdminPolicy";
    public const string UserAndAdminPolicy = "UserAndAdminPolicy";
    public const string UserAndModeratorAndAdminPolicy = "UserAndModeratorAndAdminPolicy";
}
