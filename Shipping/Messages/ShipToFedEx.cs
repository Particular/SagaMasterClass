namespace Shipping.Messages
{
    public class ShipToFedEx : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}