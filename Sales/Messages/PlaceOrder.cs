namespace Sales.Messages
{
    using NServiceBus;

    class PlaceOrder : IMessage
    {
        public string OrderId { get; set; }
    }
}