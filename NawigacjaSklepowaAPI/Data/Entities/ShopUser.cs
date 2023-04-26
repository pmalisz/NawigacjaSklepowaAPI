namespace NawigacjaSklepowaAPI.Data.Entities
{
    public class ShopUser : BaseEntity
    {
        public virtual Shop Shop { get; set; }
        public int ShopId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }

    }
}