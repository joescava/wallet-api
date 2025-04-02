using Xunit;
using Moq;
using WalletApi.Application.DTOs;
using WalletApi.Application.Interfaces;
using WalletApi.Domain.Entities;
using WalletApi.Infrastructure.Data;
using WalletApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class TransactionServiceTests
{
    private readonly TransactionService _transactionService;
    private readonly Mock<IWalletRepository> _walletRepoMock;
    private readonly WalletDbContext _dbContext;

    public TransactionServiceTests()
    {
        var options = new DbContextOptionsBuilder<WalletDbContext>()
            .UseInMemoryDatabase(databaseName: "WalletDb_Test")
            .Options;

        _walletRepoMock = new Mock<IWalletRepository>();
        _dbContext = new WalletDbContext(options);
        _transactionService = new TransactionService(_walletRepoMock.Object, _dbContext);
    }

    [Fact]
public async Task AddTransactionAsync_Credit_ShouldIncreaseBalance()
{
    // Arrange
    var wallet = new Wallet
    {
        Id = 1,
        DocumentId = "1234567890",
        Name = "Johan",
        Balance = 1000,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    // Mock: repositorio devuelve este wallet por ID
    _walletRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(wallet);

    // Persistencia real: InMemory necesita esto para hacer tracking 
    _dbContext.Wallets.Add(wallet);
    await _dbContext.SaveChangesAsync(); 

    var request = new CreateTransactionRequest
    {
        WalletId = 1,
        Amount = 500,
        Type = TransactionType.Credit
    };

    // Act
    await _transactionService.AddTransactionAsync(request);

    // Assert
    Assert.Equal(1500, wallet.Balance);
}
}