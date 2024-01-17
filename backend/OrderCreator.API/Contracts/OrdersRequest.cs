namespace OrderCreator.API.Contracts
{
    public record OrdersRequest(
        string FromCity,
        string FromAddress,
        string ToCity,
        string ToAddress,
        double Weight,
        DateTime PickupDate);
}
