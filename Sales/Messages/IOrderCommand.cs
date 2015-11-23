namespace Sales.Messages
{
    using NServiceBus;

    public interface IOrderCommand : ICommand
    {
        string OrderId { get; set; }
    }
}