using CampaignManagerApi.Dtos;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Mappers;

public static class CampaignMapper
{
    public static Campaign ToCampaign(this CampaignCreateUpdateDto createDto)
    {
        return new Campaign
        {
            Name = createDto.Name,
            BidAmount = createDto.BidAmount,
            Fund = createDto.Fund,
            Status = createDto.Status,
            Town = createDto.Town,
            RadiusKm = createDto.RadiusKm,
            ProductId = createDto.ProductId,
            
            Keywords = createDto.Keywords
                .Select(k => new CampaignKeyword { Name = k })
                .ToList()
        };
    }

    public static CampaignReadDto ToCampaignDto(this Campaign campaign)
    {
        return new CampaignReadDto
        {
            Id = campaign.Id,
            Name = campaign.Name,
            BidAmount = campaign.BidAmount,
            Fund = campaign.Fund,
            Status = campaign.Status,
            Town = campaign.Town,
            RadiusKm = campaign.RadiusKm,
            ProductId = campaign.ProductId,
            
            Keywords = campaign.Keywords.
                Select(k => k.Name)
                .ToList()
        };
    }
    
    public static void UpdateCampaignFromDto(this Campaign campaign, CampaignCreateUpdateDto dto)
    {
        campaign.Name = dto.Name;
        campaign.BidAmount = dto.BidAmount;
        campaign.Fund = dto.Fund;
        campaign.Status = dto.Status;
        campaign.Town = dto.Town;
        campaign.RadiusKm = dto.RadiusKm;
        campaign.ProductId = dto.ProductId;

        // Replace keywords
        campaign.Keywords = dto.Keywords
            .Select(k => new CampaignKeyword { Name = k, CampaignId = campaign.Id })
            .ToList();
    }
}