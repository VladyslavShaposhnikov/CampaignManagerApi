using System.ComponentModel.DataAnnotations;

namespace CampaignManagerApi.Dtos;

public class SellerDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Emerald Balance is required.")]
    [Range(typeof(decimal), "0.00", "1000000000.00", ErrorMessage = "Emerald Balance must be between 0.00 and 1 000 000 000.00")]
    public decimal EmeraldBalance { get; set; }
}