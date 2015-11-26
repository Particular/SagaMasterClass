namespace Shipping.Messages.FedEx
{
    public class FedExResponse : IShippingResponse
    {
        public string OrderId { get; set; }

        public string TrackingNumber { get; set; }
    }
}