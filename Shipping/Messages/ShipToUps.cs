namespace Shipping.Messages
{
    class ShipToUps : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}