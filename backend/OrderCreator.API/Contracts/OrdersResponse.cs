namespace OrderCreator.API.Contracts
{
    public record OrdersResponse(
        Guid Id,
        string FromCity,
        string FromAddress,
        string ToCity,
        string ToAddress,
        double Weight,
        DateTime PickupDate);
}
