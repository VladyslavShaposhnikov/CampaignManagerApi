using CampaignManagerApi.Dtos;
using CampaignManagerApi.Models;

namespace CampaignManagerApi.Mappers;

public static class SellerMapper
{
    public static SellerReadDto ToDto(this Seller seller)
    {
        return new SellerReadDto
        {
            Id = seller.Id,
            Name = seller.Name,
            EmeraldBalance = seller.EmeraldBalance
        };
    }

    public static Seller ToEntity(this SellerDto dto)
    {
        return new Seller
        {
            Name = dto.Name,
            EmeraldBalance = Math.Round(dto.EmeraldBalance, 2)
        };
    }

    public static void UpdateEntity(this Seller seller, SellerDto dto)
    {
        seller.Name = dto.Name;
        seller.EmeraldBalance = dto.EmeraldBalance;
    }
}