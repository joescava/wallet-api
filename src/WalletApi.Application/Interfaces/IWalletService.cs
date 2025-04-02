using WalletApi.Application.DTOs;

namespace WalletApi.Application.Interfaces;

public interface IWalletService
{
    Task<WalletDto> CreateAsync(CreateWalletRequest request);
    Task<WalletDto?> GetByIdAsync(int id);
    Task<IEnumerable<WalletDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateWalletRequest request);
    Task DeleteAsync(int id);
}