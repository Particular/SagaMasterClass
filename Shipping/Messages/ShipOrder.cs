namespace Shipping.Messages
{
    class ShipOrder : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}