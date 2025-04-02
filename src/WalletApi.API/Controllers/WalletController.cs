using Microsoft.AspNetCore.Mvc;
using WalletApi.Application.DTOs;
using WalletApi.Application.Interfaces;

namespace WalletApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
    {
        var result = await _walletService.CreateAsync(request);
        return CreatedAtAction(nameof(GetWalletById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWalletById(int id)
    {
        var result = await _walletService.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _walletService.GetAllAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWallet(int id, [FromBody] UpdateWalletRequest request)
    {
        await _walletService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWallet(int id)
    {
        await _walletService.DeleteAsync(id);
        return NoContent();
    }
}