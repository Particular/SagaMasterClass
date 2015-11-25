namespace Billing
{
    using NServiceBus;

    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}