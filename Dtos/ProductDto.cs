using System.ComponentModel.DataAnnotations;

namespace CampaignManagerApi.Dtos;

public class ProductDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters.")]
    public string Name { get; set; }
    public int SellerId { get; set; }
}