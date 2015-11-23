namespace Sales.Messages
{
    class CancelOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}