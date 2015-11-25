namespace Shipping.Messages
{
    using NServiceBus;

    public interface IShippingCommand : ICommand
    {
        string OrderId { get; set; }
    }
}