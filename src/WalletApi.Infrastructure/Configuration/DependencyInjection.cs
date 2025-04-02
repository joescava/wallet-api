using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WalletApi.Application.Interfaces;
using WalletApi.Infrastructure.Data;
using WalletApi.Infrastructure.Services;
using WalletApi.Application.Services;

namespace WalletApi.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            // Solo registra SQLite si NO es entorno de test
            if (!env.IsEnvironment("IntegrationTest"))
            {
                services.AddDbContext<WalletDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            }

            // Repositorios y servicios
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletApi.Application.Services.WalletService>();

            return services;
        }
    }
}