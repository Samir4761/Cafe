namespace Cafe.Data
{
    public class Orders
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string AddInsName { get; set; }
        public string CoffeeName { get; set; }
        public string Discount { get; set; }
        public string CoffeePrice { get; set; }
        public string AddInsPrice { get; set; }
        public string TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}