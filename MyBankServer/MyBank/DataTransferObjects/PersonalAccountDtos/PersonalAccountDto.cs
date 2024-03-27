using MyBank.Persistence.Entities;

namespace MyBank.API.DataTransferObjects.PersonalAccountDtos;

public record PersonalAccountDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsForTransfersByNickname { get; set; }
    public int? UserId { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
}
