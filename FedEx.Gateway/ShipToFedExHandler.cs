namespace FedEx.Gateway
{
    using System;
    using System.Net.Http;
    using NServiceBus;
    using Shipping.Messages.FedEx;

    public class ShipToFedExHandler : IHandleMessages<ShipUsingFedEx>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipUsingFedEx message)
        {
            Console.WriteLine("Handling ShipToFedex with id: {0}", message.OrderId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8888");

                var httpResponseMessage = client.GetAsync("/fedex/shipit").Result;
                httpResponseMessage.EnsureSuccessStatusCode();
            }

            var guid = Guid.NewGuid().ToString();
            var trackingNumber = guid.Substring(guid.Length - 7);

            Bus.Reply(new FedExResponse() { OrderId = message.OrderId, TrackingNumber = trackingNumber });
        }
    }
}