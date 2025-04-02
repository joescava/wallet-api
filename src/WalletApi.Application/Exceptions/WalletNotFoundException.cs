namespace WalletApi.Application.Exceptions;

public class WalletNotFoundException : Exception
{
    public WalletNotFoundException(int id) : base($"No se encontró una billetera con ID {id}.") { }
}