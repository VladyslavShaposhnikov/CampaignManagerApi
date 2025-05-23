namespace CampaignManagerApi.Models;

public class CampaignKeyword
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
}