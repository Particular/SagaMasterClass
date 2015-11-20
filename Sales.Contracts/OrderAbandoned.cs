namespace Sales.Contracts
{
    using NServiceBus;

    public class OrderAbandoned : IEvent
    {
        public string OrderId { get; set; }
    }
}