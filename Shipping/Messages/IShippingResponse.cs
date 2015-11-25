namespace Shipping.Messages
{
    using NServiceBus;

    public interface IShippingResponse : IMessage
    {
        string OrderId { get; set; }
    }
}