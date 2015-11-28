namespace Shipping.Messages.Ups
{
    class UpsResponse : IShippingResponse
    {
        public string TrackingCode { get; set; }
    }
}