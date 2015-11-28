namespace Shipping.Messages
{
    using NServiceBus;

    public interface IShippingResponse : IMessage
    {
        string TrackingCode { get; set; }
    }
}