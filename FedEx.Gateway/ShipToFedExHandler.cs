namespace FedEx.Gateway
{
    using System;
    using System.Net.Http;
    using NServiceBus;

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

            // Bus.Reply<FedExResponse>(m => { m.OrderId = message.OrderId; });
        }
    }
}