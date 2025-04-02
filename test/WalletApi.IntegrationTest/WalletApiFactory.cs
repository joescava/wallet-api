using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using WalletApi.Infrastructure.Data;
using WalletApi.Infrastructure.Configuration; // <-- importante

namespace WalletApi.IntegrationTest
{
    public class WalletApiFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");

            builder.ConfigureServices((context, services) =>
            {
                // Eliminar SQLite
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<WalletDbContext>)
                );
                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                var dbDescriptor = services.SingleOrDefault(
                    d => d.ImplementationType == typeof(WalletDbContext)
                );
                if (dbDescriptor != null)
                    services.Remove(dbDescriptor);

                // InMemory DB para integración
                services.AddDbContext<WalletDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationDb");
                });

                services.AddInfrastructure(context.Configuration, context.HostingEnvironment);

                // Crea la DB
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
                db.Database.EnsureCreated();
            });

            return base.CreateHost(builder);
        }
    }
}