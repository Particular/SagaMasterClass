namespace Shipping.Messages
{
    public class ShipUsingFedEx : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}