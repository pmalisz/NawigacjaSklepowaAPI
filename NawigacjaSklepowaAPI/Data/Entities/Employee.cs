namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class Employee : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
