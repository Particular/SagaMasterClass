namespace Shipping.Messages
{
    class ShipOrderResponse : IShippingResponse 
    {
        public string OrderId { get; set; }
    }
}