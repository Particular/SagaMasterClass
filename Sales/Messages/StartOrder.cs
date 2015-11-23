namespace Sales.Messages
{
    class StartOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}