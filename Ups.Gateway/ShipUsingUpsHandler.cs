namespace Ups.Gateway
{
    using System;
    using NServiceBus;
    using Shipping.Messages.Ups;

    public class ShipUsingUpsHandler : IHandleMessages<ShipUsingUps>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipUsingUps message)
        {
            Console.WriteLine($"Requesting UPS shipment for order {message.OrderId}");

            var trackingCode = "UPS-" + Guid.NewGuid().ToString().Substring(0, 7);

            Bus.Reply(new UpsResponse
            {
                TrackingNumber = trackingCode
            });
            Console.Out.WriteLine($"UPS shipment setup for order {message.OrderId}, tracking code: {trackingCode}");
        }
    }
}