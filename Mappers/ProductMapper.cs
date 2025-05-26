using CampaignManagerApi.Dtos;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Mappers;

public static class ProductMapper
{
    public static ProductReadDto ToDto(this Product product)
    {
        return new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            SellerId = product.SellerId
        };
    }

    public static Product ToEntity(this ProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            SellerId = dto.SellerId
        };
    }

    public static void UpdateEntity(this Product product, ProductDto dto)
    {
        product.Name = dto.Name;
        product.SellerId = dto.SellerId;
    }
}