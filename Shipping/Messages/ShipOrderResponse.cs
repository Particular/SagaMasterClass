namespace Shipping.Messages
{
    public class ShipOrderResponse : IShippingResponse
    {
        public string OrderId { get; set; }
        public string TrackingCode { get; set; }
    }
}