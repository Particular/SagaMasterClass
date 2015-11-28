namespace Shipping.Messages
{
    class ShipOrderResponse : IShippingResponse 
    {
        public string TrackingCode { get; set; }
        public string TrackingNumber { get; set; }
    }
}