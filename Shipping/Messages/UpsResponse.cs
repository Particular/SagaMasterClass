namespace Shipping.Messages.Ups
{
    class UpsResponse : IShippingResponse
    {
        public string OrderId { get; set; }

        public string TrackingNumber { get; set; }
    }
}