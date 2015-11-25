namespace Ups.Gateway
{
    using System;
    using NServiceBus;
    using Shipping.Messages;

    public class ShipToUpsHandler : IHandleMessages<ShipToUps>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipToUps message)
        {
            Console.WriteLine("Handling ShipToUps with id: {0}", message.OrderId);

            var guid = Guid.NewGuid().ToString();
            var trackingNumber = guid.Substring(guid.Length - 7);

            Bus.Reply(new UpsResponse { OrderId = message.OrderId, TrackingNumber = trackingNumber });
        }
    }
}