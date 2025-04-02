namespace WalletApi.Application.Exceptions;

public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException() : base("Saldo insuficiente para realizar la transacción.") { }
}