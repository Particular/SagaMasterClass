namespace Shipping.Messages
{
    public class ShipUsingUps : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}