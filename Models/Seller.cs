namespace CampaignManagerApi.Models;

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal EmeraldBalance { get; set; } = 0;
    
    public List<Product> Products { get; set; } = new();
}