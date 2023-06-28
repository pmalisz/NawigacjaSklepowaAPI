using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Shelves;

namespace NawigacjaSklepowaAPI.Models.Shops
{
    public class ShopLayoutDto
    {
        public int ShopId { get; set; }
        public List<ShelfDto> Shelves { get; set; }
    }
}
