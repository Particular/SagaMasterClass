namespace Shipping.Messages.FedEx
{
    public class ShipUsingFedEx : IShippingCommand
    {
        public string OrderId { get; set; }
    }
}