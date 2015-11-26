namespace Shipping.Messages.Ups
{
    public class ShipUsingUps : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}