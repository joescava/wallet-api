using Microsoft.AspNetCore.Mvc;
using WalletApi.Application.DTOs;
using WalletApi.Application.Interfaces;

namespace WalletApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionRequest request)
    {
        await _transactionService.AddTransactionAsync(request);
        return Ok();
    }

    [HttpGet("wallet/{walletId}")]
    public async Task<IActionResult> GetByWalletId(int walletId)
    {
        var result = await _transactionService.GetByWalletIdAsync(walletId);
        return Ok(result);
    }
}