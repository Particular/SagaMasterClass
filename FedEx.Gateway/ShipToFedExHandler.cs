namespace FedEx.Gateway
{
    using System;
    using System.Net.Http;
    using NServiceBus;
    using Shipping.Messages;

    public class ShipToFedExHandler : IHandleMessages<ShipToFedEx>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipToFedEx message)
        {
            Console.WriteLine("Handling ShipToFedex with id: {0}", message.OrderId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8888/fedex");

                client.GetAsync("/shipit").Result.EnsureSuccessStatusCode();
            }

            var guid = Guid.NewGuid().ToString();
            var trackingNumber = guid.Substring(guid.Length - 7);

            Bus.Reply(new FedExResponse() { OrderId = message.OrderId, TrackingNumber = trackingNumber });
        }
    }
}