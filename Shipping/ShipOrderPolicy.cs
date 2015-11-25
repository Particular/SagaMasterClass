namespace Shipping
{
    using System;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    class ShipOrderPolicy : Saga<ShipOrderPolicy.State>
        , IAmStartedByMessages<ShipOrder>
        , IHandleMessages<FedExResponse>
        , IHandleMessages<UpsResponse>
        , IHandleTimeouts<FedExTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<ShipOrder>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<FedExResponse>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<UpsResponse>(m => m.OrderId).ToSaga(m => m.OrderId);
        }

        public void Handle(ShipOrder message)
        {
            Data.OrderId = message.OrderId;

            Data.SentToFedex = true;

            Console.WriteLine($"Handeling ShipOrder command with id: {message.OrderId}");

            Bus.Send(new ShipToFedEx { OrderId = Data.OrderId });

            RequestTimeout(TimeSpan.FromSeconds(FedEx.TimeoutInSeconds), new FedExTimeout());
        }

        public void Handle(FedExResponse message)
        {
            Console.WriteLine($"Handling FedExResponse orderId: {message.OrderId}, tracking number: {message.TrackingNumber}, Data.OrderId: {Data.OrderId}");

            ReplyToOriginator(new ShipOrderResponse { OrderId = message.OrderId, TrackingNumber = message.TrackingNumber });

            MarkAsComplete();
        }

        public void Timeout(FedExTimeout message)
        {
            Bus.Send(new ShipToUps { OrderId = Data.OrderId });
        }

        public void Handle(UpsResponse message)
        {
            Console.WriteLine($"Handling UpsResponse orderId: {message.OrderId}, tracking number: {message.TrackingNumber}, Data.OrderId: {Data.OrderId}");

            ReplyToOriginator(new ShipOrderResponse { OrderId = message.OrderId, TrackingNumber = message.TrackingNumber });

            MarkAsComplete();
        }

        public class State : ContainSagaData
        {
            [Unique]
            public virtual string OrderId { get; set; }

            public virtual bool SentToFedex { get; set; }
        }
    }
}