namespace Shipping.Messages
{
    public class ShipToUps : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}