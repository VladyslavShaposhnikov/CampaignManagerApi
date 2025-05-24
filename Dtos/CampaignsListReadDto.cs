using CampaignManagerApi.Models;

namespace CampaignManagerApi.Dtos;

public class CampaignsListReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal BidAmount { get; set; }
    public decimal Fund { get; set; }
    public CampaignStatus Status { get; set; }
    public string Town { get; set; }
    public double RadiusKm { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public List<string> Keywords { get; set; }
}