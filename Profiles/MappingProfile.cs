using AutoMapper;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Territory, TerritoryDTOResponse>().ReverseMap();
            CreateMap<TerritoryDTORequest, Territory>().ReverseMap();

        }
    }
}
