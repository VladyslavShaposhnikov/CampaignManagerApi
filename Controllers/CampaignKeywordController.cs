using CampaignManagerApi.Data;
using CampaignManagerApi.Dtos;
using CampaignManagerApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagerApi.Controllers;
[ApiController]
[Route("api/campaign-keyword")]
public class CampaignKeywordController : ControllerBase
{
    private readonly AppDbContext _context;

    public CampaignKeywordController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> CampaignKeywords()
    {
        var campaignKeyword = await _context.CampaignKeywords.ToArrayAsync();

        var campaignKeywordDto = campaignKeyword
            .Select(c => c.ToCampaignKeywordsDto());
        
        return Ok(campaignKeywordDto);
    }

    [HttpGet("campaign/{campaignId}")]
    public async Task<IActionResult> GetKeywordsByCampaignId(int campaignId)
    {
        var campaignKeyword = await _context.CampaignKeywords
            .Where(c => c.CampaignId == campaignId)
            .ToArrayAsync();
        
        var campaignKeywordDto = campaignKeyword.
            Select(c => c.ToCampaignKeywordsDto());
        
        return Ok(campaignKeywordDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCampaignKeyword(CampaignKeywordsDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var campaignKeyword = dto.ToCampaignKeyword();
        _context.CampaignKeywords.Add(campaignKeyword);
        await _context.SaveChangesAsync();
        return Ok(campaignKeyword);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaignKeyword(int id)
    {
        var campaignKeyword = await _context.CampaignKeywords.FindAsync(id);
        if (campaignKeyword == null)
            return NotFound();

        _context.CampaignKeywords.Remove(campaignKeyword);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}