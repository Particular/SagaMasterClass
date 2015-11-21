namespace Sales.Messages
{
    using NServiceBus;

    class CancelOrder : IMessage
    {
        public string OrderId { get; set; }
    }
}