using WalletApi.Domain.Entities;

namespace WalletApi.Application.DTOs;

public class CreateTransactionRequest
{
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
}