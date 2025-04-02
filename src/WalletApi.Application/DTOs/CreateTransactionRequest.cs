using System.ComponentModel.DataAnnotations;
using WalletApi.Domain.Entities;

namespace WalletApi.Application.DTOs;

public class CreateTransactionRequest
{
    [Required]
    public int WalletId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
    public decimal Amount { get; set; }

    [Required]
    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; set; }
}