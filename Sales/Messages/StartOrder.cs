namespace Sales.Messages
{
    using NServiceBus;

    class StartOrder : IMessage
    {
        public string OrderId { get; set; }
    }
}