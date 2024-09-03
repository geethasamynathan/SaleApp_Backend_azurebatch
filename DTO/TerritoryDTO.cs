namespace SaleApp_Backend.DTO
{
    public class TerritoryDTOResponse
    {
        public string TerritoryId { get; set; } 
        public string TerritoryDescription { get; set; } 
    }
    public class TerritoryDTORequest
    {
        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
    }
}
