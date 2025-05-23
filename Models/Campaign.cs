namespace CampaignManagerApi.Models;

public class Campaign
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal BidAmount { get; set; }
    public decimal Fund { get; set; }
    public CampaignStatus Status { get; set; } 
    public string Town { get; set; }
    public double RadiusKm { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public List<CampaignKeyword> Keywords { get; set; } = new();
}