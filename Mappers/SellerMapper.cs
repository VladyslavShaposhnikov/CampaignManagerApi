using CampaignManagerApi.Dtos;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Mappers;

public static class SellerMapper
{
    public static SellerReadDto ToDto(Seller seller)
    {
        return new SellerReadDto
        {
            Id = seller.Id,
            Name = seller.Name,
            EmeraldBalance = seller.EmeraldBalance
        };
    }

    public static Seller ToEntity(SellerDto dto)
    {
        return new Seller
        {
            Name = dto.Name,
            EmeraldBalance = dto.EmeraldBalance
        };
    }

    public static void UpdateEntity(Seller seller, SellerDto dto)
    {
        seller.Name = dto.Name;
        seller.EmeraldBalance = dto.EmeraldBalance;
    }
}