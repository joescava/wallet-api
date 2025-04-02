using WalletApi.Domain.Entities;

namespace WalletApi.Application.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(int id);
    Task<IEnumerable<Wallet>> GetAllAsync();
    Task<Wallet> CreateAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
    Task DeleteAsync(Wallet wallet);
}