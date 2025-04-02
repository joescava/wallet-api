using Xunit;
using Moq;
using WalletApi.Application.Services;
using WalletApi.Application.DTOs;
using WalletApi.Application.Interfaces;
using WalletApi.Domain.Entities;
using System.Threading.Tasks;

public class WalletServiceTests
{
    private readonly Mock<IWalletRepository> _walletRepoMock;
    private readonly WalletService _walletService;

    public WalletServiceTests()
    {
        _walletRepoMock = new Mock<IWalletRepository>();
        _walletService = new WalletService(_walletRepoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnWalletDto()
    {
        // Arrange
        var request = new CreateWalletRequest
        {
            DocumentId = "1234567890",
            Name = "Johan",
            InitialBalance = 1000
        };

        _walletRepoMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Wallet>()))
            .ReturnsAsync((Wallet wallet) => 
            {
                wallet.Id = 1;
                return wallet;
            });

        // Act
        var result = await _walletService.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1234567890", result.DocumentId);
        Assert.Equal("Johan", result.Name);
        Assert.Equal(1000, result.Balance);
    }
}