using CampaignManagerApi.Dtos;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Mappers;

public static class CampaignKeywordMapper
{
    public static CampaignKeyword ToCampaignKeyword(this CampaignKeywordsDto campaignKeywordsDto)
    {
        return new CampaignKeyword
        {
            Name = campaignKeywordsDto.Name,
            CampaignId = campaignKeywordsDto.CampaignId
        };
    }

    public static CampainKeywordsReadDto ToCampaignKeywordsDto(this CampaignKeyword campaignKeyword)
    {
        return new CampainKeywordsReadDto
        {
            Id = campaignKeyword.Id,
            Name = campaignKeyword.Name,
            CampaignId = campaignKeyword.CampaignId
        };
    }
}