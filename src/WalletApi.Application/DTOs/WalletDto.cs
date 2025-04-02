namespace WalletApi.Application.DTOs;

public class WalletDto
{
    public int Id { get; set; }
    public string DocumentId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}