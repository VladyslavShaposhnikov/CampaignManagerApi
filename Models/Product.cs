namespace CampaignManagerApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
    
    public List<Campaign> Campaigns { get; set; } = new();
}