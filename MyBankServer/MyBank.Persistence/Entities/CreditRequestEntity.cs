using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CreditRequestEntity
{
    public CreditRequestEntity() { }

    public CreditRequestEntity(int id, string name, DateTime creationDate, bool isActive, bool? isApproved, int? creditPackageId, CreditPackageEntity? creditPackage, int? moderatorId, ModeratorEntity? moderator, int? userId, UserEntity? user)
    {
        Id = id;
        Name = name;
        CreationDate = creationDate;
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
    public DateTime CreationDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool? IsApproved { get; set; } = null;
    public int? CreditPackageId { get; set; } = null;
    public CreditPackageEntity? CreditPackage { get; set; } = null;
    public int? ModeratorId { get; set; } = null;
    public ModeratorEntity? Moderator { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
}
