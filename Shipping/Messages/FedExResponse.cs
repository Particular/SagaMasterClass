namespace Shipping.Messages.FedEx
{
    public class FedExResponse : IShippingResponse
    {
        public string TrackingCode { get; set; }
    }
}