using WalletApi.Application.DTOs;

namespace WalletApi.Application.Interfaces;

public interface ITransactionService
{
    Task AddTransactionAsync(CreateTransactionRequest request);
    Task<IEnumerable<TransactionDto>> GetByWalletIdAsync(int walletId);
}