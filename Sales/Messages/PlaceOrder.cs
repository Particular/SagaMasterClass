namespace Sales.Messages
{
    class PlaceOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}