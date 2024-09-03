using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using System.Reflection.Metadata.Ecma335;

namespace SaleApp_Backend.Services
{
    public class TerritoryService : ITerritoryService
    {
        private readonly NorthwindPubsContext _context;
        private readonly IMapper _mapper;
        public TerritoryService(NorthwindPubsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Territory> CreateNewTerritory(TerritoryDTORequest territoryDTORequest)
        {
            Territory territory = new Territory();
            try
            {
                if(territoryDTORequest != null)
                {
                   territory= _mapper.Map<Territory>(territoryDTORequest);
                   await _context.Territories.AddAsync(territory);
                    _context.SaveChanges();
                }
                return territory;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task  <List<TerritoryDTOResponse>> GetAllTerritories()
        {
            var territories = await _context.Territories.ToListAsync();
            return _mapper.Map<List<TerritoryDTOResponse>>(territories);

                   }

        public async Task<Territory> GetTerritoryById(string id)
        {
           Territory territory= await _context.Territories.FirstOrDefaultAsync
                ( t=> t.TerritoryId == id);
            if (territory != null)
                return territory;
            else
                return null;
        }

        public async Task<Territory> UpdateTerritory(Territory territory)
        {
            var result=await _context.Territories.FirstOrDefaultAsync(t => 
            t.TerritoryId==territory.TerritoryId);
            if (result != null)
            {
                result.TerritoryDescription = territory.TerritoryDescription;
                result.RegionId = territory.RegionId;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

    }
}
