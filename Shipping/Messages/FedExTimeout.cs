namespace Shipping.Messages.FedEx
{
    class FedExTimeout : IShippingResponse
    {
        public string TrackingCode { get; set; }
    }
}