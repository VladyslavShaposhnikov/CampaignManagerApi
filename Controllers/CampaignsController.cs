using CampaignManagerApi.Data;
using CampaignManagerApi.Dtos;
using CampaignManagerApi.Mappers;
using CampaignManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagerApi.Controllers;

[ApiController]
[Route("api/campaigns")]
public class CampaignsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CampaignsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCampaigns()
    {
        var campaigns = await _context.Campaigns
            .Include(c => c.Keywords)
            .Include(c => c.Product)
            .ToArrayAsync();
        var campaignsDto = campaigns.Select(c => c.ToListCampaignDto());
        return Ok(campaignsDto);
    }
    
    [HttpGet("active")]  
    public async Task<IActionResult> GetActiveCampaigns()
    {
        var campaigns = await _context.Campaigns
            .Where(c => c.Status == CampaignStatus.On)
            .Include(c => c.Keywords)
            .Include(c => c.Product)
            .ToArrayAsync();
        var campaignsDto = campaigns.Select(c => c.ToListCampaignDto());
        return Ok(campaignsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCampaignById(int id)
    {
        var campaign = await _context.Campaigns
            .Include(c => c.Keywords)
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
        {
            return NotFound();
        }
        
        var campaignDto = campaign.ToCampaignDto();
        return Ok(campaignDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCampaign(CampaignCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var campaign = dto.ToCampaign();
        var seller = await _context.Sellers
            .FirstOrDefaultAsync(s => s.Products.Any(p => p.Id == dto.ProductId));
        if (seller == null)
        {
            return NotFound();
        }
        
        if (dto.Fund > seller.EmeraldBalance)
        {
            return BadRequest(new { errors = new[] { "Fund is more than emerald" } });
        }

        if (dto.Fund < dto.BidAmount)
        {
            return BadRequest(new { errors = new[] { "Fund is less than bid" } });
        }
        
        seller.EmeraldBalance -= dto.Fund;
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.Id }, campaign.ToCampaignDto());
    }
    
    [HttpPost("transmit/{id}")]
    public async Task<IActionResult> CampaignDecreaseFund(int id)
    {
        var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == id);
        if (campaign == null || campaign.Status == CampaignStatus.Off)
        {
            return BadRequest(new { errors = new[] { "Status is Off" } });
        }
        if (campaign.Fund < campaign.BidAmount * 2)
        {
            campaign.Status = CampaignStatus.Off;
        }
        campaign.Fund -= campaign.BidAmount;
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
        return Ok(new { fund = campaign.Fund });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCampaign(int id, CampaignCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var campaign = await _context.Campaigns
            .Include(c => c.Keywords)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);
        
        if (product == null)
        {
            return NotFound();
        }
        var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Products.Contains(product));
        
        if (seller == null || campaign == null)
        {
            return NotFound();
        }
        
        if (dto.Fund - campaign.Fund > seller.EmeraldBalance || dto.Fund < campaign.Fund)
        {
            return BadRequest(new { errors = new[] { "You dont have enough money on your account or you try to decrease fund" } });
        }
        if (dto.Fund < dto.BidAmount)
        {
            return BadRequest(new { errors = new[] { "Fund is less than bid" } });
        }
        seller.EmeraldBalance -= (dto.Fund - campaign.Fund);
        
        // Clear old keywords
        _context.CampaignKeywords.RemoveRange(campaign.Keywords);
        // Update
        campaign.UpdateCampaignFromDto(dto);

        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }

        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}