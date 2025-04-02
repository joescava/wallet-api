using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using WalletApi.Application.DTOs;


namespace WalletApi.IntegrationTest
{
    public class WalletEndpointsTests : IClassFixture<WalletApiFactory>
    {
        private readonly HttpClient _client;

        public WalletEndpointsTests(WalletApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateWallet_ReturnsCreatedWallet()
        {
            // Arrange
            var request = new CreateWalletRequest
            {
                DocumentId = "9876543210",
                Name = "Test Wallet",
                InitialBalance = 5000
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/wallet", request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"❌ API Error Body:\n{error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"❌ ERROR: {response.StatusCode} - {responseContent}");

            // Assert
            response.EnsureSuccessStatusCode(); // Valida que fue 200/201
            var created = await response.Content.ReadFromJsonAsync<WalletDto>();
            Assert.NotNull(created);
            Assert.Equal("9876543210", created.DocumentId);
            Assert.Equal("Test Wallet", created.Name);
            Assert.Equal(5000, created.Balance);
        }
    }
}