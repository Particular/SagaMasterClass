namespace Shipping
{
    using System;
    using Messages;
    using Messages.FedEx;
    using Messages.Ups;
    using NServiceBus;
    using NServiceBus.Saga;

    class ShipOrderPolicy : Saga<ShipOrderPolicy.State>,
        IAmStartedByMessages<ShipOrder>,
        IHandleMessages<FedExResponse>,
        IHandleMessages<UpsResponse>,
        IHandleTimeouts<FedExTimeout>,
        IHandleSagaNotFound
    {
        public void Handle(ShipOrder message)
        {
            Data.OrderId = message.OrderId;

            Console.WriteLine($"Requesting Fedex shipment for order {message.OrderId}");

            Bus.Send(new ShipUsingFedEx
            {
                OrderId = Data.OrderId
            });

            RequestTimeout(TimeSpan.FromSeconds(FedEx.TimeoutInSeconds), new FedExTimeout());
        }

        public void Handle(FedExResponse message)
        {
            Console.WriteLine($"Fedex shipment setup for order {Data.OrderId}, tracking code: {message.TrackingCode}");

            ReplyToOriginator(new ShipOrderResponse
            {
                TrackingCode = message.TrackingCode,
                OrderId = Data.OrderId
            });

            MarkAsComplete();
        }

        public void Handle(UpsResponse message)
        {
            Console.WriteLine($"UPS shipment setup for order {Data.OrderId}, tracking code: {message.TrackingCode}");

            ReplyToOriginator(new ShipOrderResponse
            {
                TrackingCode = message.TrackingCode
            });

            MarkAsComplete();
        }

        public void Handle(object message)
        {
            var fedExResponse = message as FedExResponse;

            if (fedExResponse != null)
            {
                Console.Out.WriteLine($"Got a late response from Fedex, tracking code: {fedExResponse.TrackingCode}");
            }

            var upsResponse = message as UpsResponse;

            if (upsResponse != null)
            {
                Console.Out.WriteLine($"Got a response from UPS but fedex already setup, tracking code: {upsResponse.TrackingCode}");
            }
        }

        public void Timeout(FedExTimeout message)
        {
            Console.WriteLine($"No response from Fedex for {Data.OrderId}, trying UPS instead");

            Bus.Send(new ShipUsingUps
            {
                OrderId = Data.OrderId
            });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<ShipOrder>(m => m.OrderId).ToSaga(m => m.OrderId);
        }

        public class State : ContainSagaData
        {
            [Unique]
            public virtual string OrderId { get; set; }
        }
    }
}