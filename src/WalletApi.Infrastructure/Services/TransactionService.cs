using WalletApi.Application.DTOs;
using WalletApi.Application.Exceptions;
using WalletApi.Application.Interfaces;
using WalletApi.Domain.Entities;
using WalletApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WalletApi.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly IWalletRepository _walletRepository;
    private readonly WalletDbContext _context;

    public TransactionService(IWalletRepository walletRepository, WalletDbContext context)
    {
        _walletRepository = walletRepository;
        _context = context;
    }

    public async Task AddTransactionAsync(CreateTransactionRequest request)
    {
        var wallet = await _walletRepository.GetByIdAsync(request.WalletId)
            ?? throw new WalletNotFoundException(request.WalletId);

        if (request.Type == TransactionType.Debit && wallet.Balance < request.Amount)
        {
            throw new InsufficientBalanceException();
        }

        var transaction = new TransactionHistory
        {
            WalletId = wallet.Id,
            Amount = request.Amount,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        // Actualizar saldo
        wallet.Balance += request.Type == TransactionType.Credit
            ? request.Amount
            : -request.Amount;

        wallet.UpdatedAt = DateTime.UtcNow;

        _context.Transactions.Add(transaction);
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransactionDto>> GetByWalletIdAsync(int walletId)
    {
        var wallet = await _walletRepository.GetByIdAsync(walletId)
            ?? throw new WalletNotFoundException(walletId);

        var transactions = _context.Transactions
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                CreatedAt = t.CreatedAt
            });

        return await transactions.ToListAsync();
    }
}