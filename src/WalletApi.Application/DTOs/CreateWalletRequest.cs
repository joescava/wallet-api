using System.ComponentModel.DataAnnotations;

namespace WalletApi.Application.DTOs;

public class CreateWalletRequest
{
    [Required]
    [StringLength(20)]
    public string DocumentId {get; set; } = default!;
    
    [Required]
    [StringLength(100)]
    public string Name {get; set; } = default!;

    [Range(0, double.MaxValue, ErrorMessage = "El saldo inicial debe ser mayor o igual a cero.")]
    public decimal InitialBalance {get; set; }
}