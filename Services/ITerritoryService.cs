using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public interface ITerritoryService
    {
        Task<List<TerritoryDTOResponse>> GetAllTerritories();
        Task<Territory> GetTerritoryById(string id); 
        Task<Territory> CreateNewTerritory(TerritoryDTORequest territoryDTORequest);
        Task <Territory> UpdateTerritory(Territory territory);
    }
}
