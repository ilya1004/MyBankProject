namespace MyBank.Domain.Models;

public class CreditRequest
{
    public CreditRequest() { }

    public CreditRequest(int id, string name, bool isActive, bool? isApproved, int? creditPackageId, CreditPackage? creditPackage, int? moderatorId, Moderator? moderator, int? userId, User? user)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
        IsApproved = isApproved;
        CreditPackageId = creditPackageId;
        CreditPackage = creditPackage;
        ModeratorId = moderatorId;
        Moderator = moderator;
        UserId = userId;
        User = user;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool? IsApproved { get; set; } = null;
    public int? CreditPackageId { get; set; } = null;
    public CreditPackage? CreditPackage { get; set; } = null;
    public int? ModeratorId { get; set; } = null;
    public Moderator? Moderator { get; set; } = null;
    public int? UserId { get; set; } = null;
    public User? User { get; set; } = null;
}
