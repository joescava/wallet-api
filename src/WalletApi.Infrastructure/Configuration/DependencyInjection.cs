using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletApi.Application.Interfaces;
using WalletApi.Infrastructure.Data;
using WalletApi.Infrastructure.Services;

namespace WalletApi.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Base de datos (SQLite por simplicidad)
            services.AddDbContext<WalletDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>();
            return services;
        }
    }
}