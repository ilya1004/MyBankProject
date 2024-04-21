using MyBank.Persistence.Entities;

namespace MyBank.API.DataTransferObjects.PersonalAccountDtos;

public record PersonalAccountDto
{
    public string Name { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
}
