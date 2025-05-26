using CampaignManagerApi.Data;
using CampaignManagerApi.Dtos;
using CampaignManagerApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagerApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        var productsDto = products.Select(p => p.ToDto());
        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var productDto = product.ToDto();
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductDto dto)
    {
        var product = ProductMapper.ToEntity(dto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        var productDto = product.ToDto();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, productDto);
    }
    
    [HttpGet("{id}/campaigns")]
    public async Task<IActionResult> GetCampaignsByProductId(int id)
    {
        var campaigns = await _context.Campaigns
            .Where(c => c.ProductId == id)
            .Include(k => k.Keywords)
            .ToListAsync();
        var campaignsDto = campaigns.Select(c => c.ToCampaignDto());
        return Ok(campaignsDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        product.UpdateEntity(dto);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
}