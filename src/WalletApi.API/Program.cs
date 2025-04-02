using WalletApi.Infrastructure.Configuration;
using WalletApi.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Registramos controladores explícitamente
builder.Services.AddControllers();

// Swagger y documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias (Infraestructura)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Esta línea mapea todos los controladores
app.MapControllers();

app.Run();