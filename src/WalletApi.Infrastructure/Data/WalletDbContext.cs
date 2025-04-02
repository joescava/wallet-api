using Microsoft.EntityFrameworkCore;
using WalletApi.Domain.Entities;

namespace WalletApi.Infrastructure.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<TransactionHistory> Transactions => Set<TransactionHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.DocumentId).IsRequired();
                entity.Property(w => w.Name).IsRequired();
                entity.Property(w => w.Balance).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(t => t.Wallet)
                      .WithMany(w => w.Transactions)
                      .HasForeignKey(t => t.WalletId);
            });
        }
    }
}