using Microsoft.EntityFrameworkCore;
using WalletApi.Application.Interfaces;
using WalletApi.Domain.Entities;

namespace WalletApi.Infrastructure.Data
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;

        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByIdAsync(int id)
        {
            return await _context.Wallets.FindAsync(id);
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        public async Task<Wallet> CreateAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
        }
    }
}