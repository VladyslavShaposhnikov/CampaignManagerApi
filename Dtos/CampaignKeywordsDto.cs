using System.ComponentModel.DataAnnotations;

namespace CampaignManagerApi.Dtos;

public class CampaignKeywordsDto
{
    [Required(ErrorMessage = "Town is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters.")]
    public string Name { get; set; }
    public int CampaignId { get; set; }
}