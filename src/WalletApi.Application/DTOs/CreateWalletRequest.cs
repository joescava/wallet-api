namespace WalletApi.Application.DTOs;

public class CreateWalletRequest
{
    public string DocumentId {get; set; } = default!;
    public string Name {get; set; } = default!;
    public decimal InitialBalance {get; set; }
}