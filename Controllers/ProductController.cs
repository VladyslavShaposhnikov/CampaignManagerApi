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
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products.Select(ProductMapper.ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> Get(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return Ok(ProductMapper.ToDto(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> Create(ProductDto dto)
    {
        var product = ProductMapper.ToEntity(dto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, ProductMapper.ToDto(product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        ProductMapper.UpdateEntity(product, dto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}