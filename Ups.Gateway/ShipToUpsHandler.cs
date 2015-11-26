namespace Ups.Gateway
{
    using System;
    using NServiceBus;
    using Shipping.Messages.Ups;

    public class ShipToUpsHandler : IHandleMessages<ShipUsingUps>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipUsingUps message)
        {
            Console.WriteLine("Handling ShipUsingUps with id: {0}", message.OrderId);

            var guid = Guid.NewGuid().ToString();
            var trackingNumber = guid.Substring(guid.Length - 7);

            Bus.Reply(new UpsResponse { OrderId = message.OrderId, TrackingNumber = trackingNumber });
        }
    }
}