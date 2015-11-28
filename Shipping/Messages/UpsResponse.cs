namespace Shipping.Messages.Ups
{
    class UpsResponse : IShippingResponse
    {
        public string TrackingCode { get; set; }

        public string TrackingNumber { get; set; }
    }
}