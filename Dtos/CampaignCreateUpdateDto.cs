using System.ComponentModel.DataAnnotations;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Dtos;

public class CampaignCreateUpdateDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Bid amount is required.")]
    [Range(typeof(decimal), "0.01", "1000000.00", ErrorMessage = "Bid amount must be between 0.01 and 1 000 000.00")]
    public decimal BidAmount { get; set; }
    [Required(ErrorMessage = "Fund is required.")]
    [Range(typeof(decimal), "0.01", "100000000.00", ErrorMessage = "Fund must be between 0.01 and 100 000 000.00")]
    public decimal Fund { get; set; }
    public CampaignStatus Status { get; set; }
    [Required(ErrorMessage = "Town is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters.")]
    public string Town { get; set; }
    [Required(ErrorMessage = "Radius is required.")]
    [Range(typeof(int), "0", "20015", ErrorMessage = "Radius amount must be between 0 and 20 015 km.")]
    public int RadiusKm { get; set; }
    public int ProductId { get; set; }
    [Required(ErrorMessage = "At least 1 keyword is required.")]
    public List<string> Keywords { get; set; }
}