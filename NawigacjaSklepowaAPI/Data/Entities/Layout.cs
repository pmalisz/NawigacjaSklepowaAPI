namespace NawigacjaSklepowaAPI.Data.Entities

{
    public class Layout : BaseEntity
    {
        public string Canvas { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}

