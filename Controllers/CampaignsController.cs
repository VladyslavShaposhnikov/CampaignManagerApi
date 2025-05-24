using CampaignManagerApi.Data;
using CampaignManagerApi.Dtos;
using CampaignManagerApi.Mappers;
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCampaignById(int id)
    {
        var campaign = await _context.Campaigns
            .Include(c => c.Keywords)
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (campaign == null)
            return NotFound();
        
        var campaignDto = campaign.ToCampaignDto();
        return Ok(campaignDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCampaign(CampaignCreateUpdateDto dto)
    {
        var campaign = dto.ToCampaign();
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.Id }, campaign.ToCampaignDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCampaign(int id, CampaignCreateUpdateDto dto)
    {
        var campaign = await _context.Campaigns
            .Include(c => c.Keywords)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
            return NotFound();

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
            return NotFound();

        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}