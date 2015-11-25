namespace Shipping.Messages
{
    class UpsResponse : IShippingResponse
    {
        public string OrderId { get; set; }

        public string TrackingNumber { get; set; }
    }
}