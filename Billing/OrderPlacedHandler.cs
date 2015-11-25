namespace Billing
{
    using System;
    using System.Threading;
    using NServiceBus;
    using Sales.Contracts;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderPlaced message)
        {
            Console.Out.WriteLine("Billing started for order");
            Thread.Sleep(5000);
            Console.Out.WriteLine("Billing complete for order");

            Bus.Publish(new OrderBilled
            {
                OrderId = message.OrderId
            });
        }
    }
}