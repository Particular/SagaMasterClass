namespace Shipping
{
    using System;
    using Billing;
    using Messages;
    using NServiceBus.Saga;
    using Sales.Contracts;

    public class ShippingPolicy : Saga<ShippingPolicy.State>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>
    {
        public void Handle(OrderBilled message)
        {
            Data.OrderId = message.OrderId;
            Data.Billed = true;

            if (Data.Placed)
            {
                InitiateShipping();
            }
        }

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;
            Data.Placed = true;

            if (Data.Billed)
            {
                InitiateShipping();
            }
        }

        void InitiateShipping()
        {
            Console.Out.WriteLine($"Initiating shipping for order {Data.OrderId}");
            Bus.Send(new ShipOrder
            {
                OrderId = Data.OrderId
            });
            Console.Out.WriteLine($"Initiating shipping for order {Data.OrderId}");
        }


        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(m => m.OrderId)
                .ToSaga(s => s.OrderId);

            mapper.ConfigureMapping<OrderBilled>(m => m.OrderId)
                .ToSaga(s => s.OrderId);
        }

        public class State : ContainSagaData
        {
            [Unique]
            public virtual string OrderId { get; set; }

            public virtual bool Placed { get; set; }
            public virtual bool Billed { get; set; }
        }
    }
}