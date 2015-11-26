namespace Shipping.Messages.FedEx
{
    class FedExTimeout : IShippingResponse
    {
        public string OrderId { get; set; }
    }
}