using WalletApi.Application.DTOs;
using WalletApi.Application.Exceptions;
using WalletApi.Application.Interfaces;
using WalletApi.Domain.Entities;

namespace WalletApi.Application.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<WalletDto> CreateAsync(CreateWalletRequest request)
    {
        var wallet = new Wallet
        {
            DocumentId = request.DocumentId,
            Name = request.Name,
            Balance = request.InitialBalance,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _walletRepository.CreateAsync(wallet);

        return new WalletDto
        {
            Id = created.Id,
            DocumentId = created.DocumentId,
            Name = created.Name,
            Balance = created.Balance,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }

    public async Task<WalletDto?> GetByIdAsync(int id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);
        if (wallet == null) return null;

        return new WalletDto
        {
            Id = wallet.Id,
            DocumentId = wallet.DocumentId,
            Name = wallet.Name,
            Balance = wallet.Balance,
            CreatedAt = wallet.CreatedAt,
            UpdatedAt = wallet.UpdatedAt
        };
    }

    public async Task<IEnumerable<WalletDto>> GetAllAsync()
    {
        var wallets = await _walletRepository.GetAllAsync();
        return wallets.Select(w => new WalletDto
        {
            Id = w.Id,
            DocumentId = w.DocumentId,
            Name = w.Name,
            Balance = w.Balance,
            CreatedAt = w.CreatedAt,
            UpdatedAt = w.UpdatedAt
        });
    }

    public async Task UpdateAsync(int id, UpdateWalletRequest request)
    {
        var wallet = await _walletRepository.GetByIdAsync(id)
            ?? throw new WalletNotFoundException(id);

        wallet.Name = request.Name;
        wallet.UpdatedAt = DateTime.UtcNow;

        await _walletRepository.UpdateAsync(wallet);
    }

    public async Task DeleteAsync(int id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id)
            ?? throw new WalletNotFoundException(id);

        await _walletRepository.DeleteAsync(wallet);
    }
}