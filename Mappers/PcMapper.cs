using PJATK_APBD_Cw4_s29756.DTOs.Requests;
using PJATK_APBD_Cw4_s29756.DTOs.Responses;
using PJATK_APBD_Cw4_s29756.Models;

namespace PJATK_APBD_Cw4_s29756.Mappers;

public static class PcMapper
{
    public static PcResponse ToResponse(this Pc pc)
    {
        return new PcResponse
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public static PcDetailsResponse ToDetailsResponse(this Pc pc)
    {
        return new PcDetailsResponse
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PcComponents.Select(pcComponent => new ComponentInPcResponse
            {
                Amount = pcComponent.Amount,
                Component = new ComponentResponse
                {
                    Code = pcComponent.Component.Code,
                    Name = pcComponent.Component.Name,
                    Description = pcComponent.Component.Description,
                    Manufacturer = new ManufacturerResponse
                    {
                        Id = pcComponent.Component.Manufacturer.Id,
                        Abbreviation = pcComponent.Component.Manufacturer.Abbreviation,
                        FullName = pcComponent.Component.Manufacturer.FullName,
                        FoundationDate = pcComponent.Component.Manufacturer.FoundationDate
                    },
                    Type = new ComponentTypeResponse
                    {
                        Id = pcComponent.Component.Type.Id,
                        Abbreviation = pcComponent.Component.Type.Abbreviation,
                        Name = pcComponent.Component.Type.Name
                    }
                }
            }).ToList()
        };
    }

    public static Pc ToEntity(this PcRequest request)
    {
        return new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };
    }
}
