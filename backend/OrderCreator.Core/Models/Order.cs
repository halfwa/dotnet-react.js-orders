namespace OrderCreator.Core.Models
{
    public class Order  
    {
        public const int MAX_ORDER_WEIGHT = 1000;

        private Order(
            Guid id,
            string fromCity, 
            string fromAddress,
            string toCity, 
            string toAddress,
            double weight, 
            DateTime pickupDate)
        {
            Id = id;
            FromCity = fromCity;
            FromAddress = fromAddress;
            ToCity = toCity;
            ToAddress = toAddress;
            Weight = weight;
            PickupDate = pickupDate;
        }

        public Guid Id { get; private init; }
        public string FromCity { get; } = string.Empty;
        public string FromAddress { get; } = string.Empty;
        public string ToCity { get; } = string.Empty;
        public string ToAddress { get; } = string.Empty;
        public double Weight { get; }
        public DateTime PickupDate { get; }

        public static (Order Order, string Error) Create(
            Guid id,
            string fromCity,
            string fromAddress,
            string toCity,
            string toAddress,
            double weight,
            DateTime pickupDate
            )
        {
            var error = string.Empty;

            if ( weight <= 0 || weight > MAX_ORDER_WEIGHT)
            {
                error = "Weight of order can not be less then 0 or more then 1000";
            }

            var order = new Order(id, fromCity, fromAddress, toCity, toAddress, weight, pickupDate);

            return (order, error);
        }
    }

   
}
