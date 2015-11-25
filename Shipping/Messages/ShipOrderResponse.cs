namespace Shipping.Messages
{
    class ShipOrderResponse : IShippingResponse 
    {
        public string OrderId { get; set; }
        public string TrackingNumber { get; set; }
    }
}