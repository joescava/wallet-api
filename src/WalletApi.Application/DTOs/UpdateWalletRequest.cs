using System.ComponentModel.DataAnnotations;

namespace WalletApi.Application.DTOs;

public class UpdateWalletRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name {get; set; } = default!;
}