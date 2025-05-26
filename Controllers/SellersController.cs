using CampaignManagerApi.Data;
using CampaignManagerApi.Dtos;
using CampaignManagerApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagerApi.Controllers;

[ApiController]
[Route("api/sellers")]
public class SellerController : ControllerBase
{
    private readonly AppDbContext _context;

    public SellerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var sellers = await _context.Sellers.ToListAsync();
        return Ok(sellers.Select(SellerMapper.ToDto));
    }
    
    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProductsBySeller(int id)
    {
        var products = await _context.Products
            .Where(p => p.SellerId == id)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null) return NotFound();
        return Ok(SellerMapper.ToDto(seller));
    }

    [HttpPost]
    public async Task<ActionResult> Create(SellerDto dto)
    {
        var seller = SellerMapper.ToEntity(dto);
        _context.Sellers.Add(seller);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = seller.Id }, SellerMapper.ToDto(seller));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SellerDto dto)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null) return NotFound();
        SellerMapper.UpdateEntity(seller, dto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null) return NotFound();
        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}