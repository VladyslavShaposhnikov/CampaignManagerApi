namespace CampaignManagerApi.Dtos;

public class ProductWithSellerReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SellerName { get; set; }
    public int SellerId { get; set; }
}