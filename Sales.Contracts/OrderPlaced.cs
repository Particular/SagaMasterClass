namespace Sales.Contracts
{
    using System;
    using NServiceBus;

    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public double OrderValue { get; set; }
        public DateTime OrderDate { get; set; }
    }
}