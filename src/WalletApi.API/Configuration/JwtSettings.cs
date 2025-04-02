namespace WalletApi.API.Configuration;

public class JwtSettings
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = "wallet-api";
    public string Audience { get; set; } = "wallet-api-client";
}