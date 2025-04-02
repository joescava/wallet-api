using WalletApi.Infrastructure.Configuration;
using WalletApi.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Registramos controladores explícitamente
builder.Services.AddControllers();

// Swagger y documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WalletApi.API", Version = "v1" });
    c.UseInlineDefinitionsForEnums();
});

// Inyección de dependencias (Infraestructura)
if (!builder.Environment.IsEnvironment("IntegrationTest"))
{
    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
}

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

public partial class Program { }