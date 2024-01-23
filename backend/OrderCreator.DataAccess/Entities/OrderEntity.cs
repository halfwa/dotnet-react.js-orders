namespace OrderCreator.DataAccess.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string FromCity { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string ToCity { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public double Weight { get; set; }
        public DateTime PickupDate { get; set;}
 
    }
}
