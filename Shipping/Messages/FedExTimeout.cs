namespace Shipping.Messages
{
    class FedExTimeout : IShippingResponse
    {
        public string OrderId { get; set; }
    }
}