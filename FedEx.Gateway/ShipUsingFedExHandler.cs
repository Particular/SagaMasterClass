namespace FedEx.Gateway
{
    using System;
    using System.Net.Http;
    using NServiceBus;
    using Shipping.Messages.FedEx;

    public class ShipUsingFedExHandler : IHandleMessages<ShipUsingFedEx>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipUsingFedEx message)
        {
            Console.WriteLine($"Requesting Fedex shipment for order {message.OrderId}");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8888");

                var httpResponseMessage = client.GetAsync("/fedex/shipit").Result;
                httpResponseMessage.EnsureSuccessStatusCode();
            }

            var trackingCode = "FEDEX-" + Guid.NewGuid().ToString().Substring(0, 7);

            Bus.Reply(new FedExResponse
            {
                TrackingCode = trackingCode
            });

            Console.Out.WriteLine($"Fedex shipment setup for order {message.OrderId}, tracking code: {trackingCode}");
        }
    }
}