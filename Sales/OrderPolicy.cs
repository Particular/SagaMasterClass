namespace Sales
{
    using System;
    using Contracts;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    class OrderPolicy : Saga<OrderPolicy.SagaState>,
        IAmStartedByMessages<StartOrder>,
        IHandleMessages<PlaceOrder>,
        IHandleMessages<CancelOrder>,
        IHandleTimeouts<OrderPolicy.MarkOrderAsAbandoned>
    {
        public void Handle(StartOrder message)
        {
            Data.OrderId = message.OrderId;
            Data.State = OrderState.Tentative;

            RequestTimeout<MarkOrderAsAbandoned>(TimeSpan.FromSeconds(10));
        }

        public void Handle(CancelOrder message)
        {
            Data.State = OrderState.Cancelled;
            Bus.Publish(new OrderCancelled
            {
                OrderId = Data.OrderId
            });
            Console.Out.WriteLine($"Order {Data.OrderId} cancelled");
        }

        public void Handle(PlaceOrder message)
        {
            Data.State = OrderState.Placed;
            Bus.Publish(new OrderPlaced
            {
                OrderId = Data.OrderId,
                CustomerId = message.CustomerId,
                OrderDate = message.OrderDate,
                OrderValue = message.OrderValue
            });
            Console.Out.WriteLine($"Order {Data.OrderId} placed");
        }


        public void Timeout(MarkOrderAsAbandoned state)
        {
            if (Data.State != OrderState.Tentative)
            {
                return;
            }

            Bus.Publish(new OrderAbandoned
            {
                OrderId = Data.OrderId
            });

            MarkAsComplete();

            Console.Out.WriteLine($"Order {Data.OrderId} abandoned");
        }


        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaState> mapper)
        {
            mapper.ConfigureMapping<StartOrder>(m => m.OrderId)
                .ToSaga(s => s.OrderId);

            mapper.ConfigureMapping<PlaceOrder>(m => m.OrderId)
                .ToSaga(s => s.OrderId);

            mapper.ConfigureMapping<CancelOrder>(m => m.OrderId)
                .ToSaga(s => s.OrderId);
        }

        public class SagaState : ContainSagaData
        {
            [Unique]
            public virtual string OrderId { get; set; }

            public virtual OrderState State { get; set; }
        }

        public class MarkOrderAsAbandoned
        {
        }
    }
}