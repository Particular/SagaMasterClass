namespace CustomerCare
{
    using System;
    using Contracts;
    using NServiceBus.Saga;
    using Sales.Contracts;

    public class PreferedCustomerPolicy : Saga<PreferedCustomerPolicy.State>,
        IAmStartedByMessages<OrderPlaced>,
        IHandleTimeouts<OrderPlaced>
    {
        public void Handle(OrderPlaced message)
        {
            Data.CustomerId = message.CustomerId;

            AdjustRunningTotal(message.OrderValue);

            RequestTimeout(message.OrderDate.AddSeconds(20), message);
        }

        public void Timeout(OrderPlaced state)
        {
            AdjustRunningTotal(-state.OrderValue);
        }

        void AdjustRunningTotal(double orderValue)
        {
            Data.YearlyRunningTotal += orderValue;
            if (Data.YearlyRunningTotal > 5000)
            {
                if (!Data.IsPrefered)
                {
                    Bus.Publish<CustomerMadePrefered>(m => m.CustomerId = Data.CustomerId);
                    Console.Out.WriteLine($"Customer {Data.CustomerId} made preferred");
                }

                Data.IsPrefered = true;
            }
            else
            {
                if (Data.IsPrefered)
                {
                    Bus.Publish<CustomerDemotedToRegularStatus>(m => m.CustomerId = Data.CustomerId);
                    Console.Out.WriteLine($"Customer {Data.CustomerId} demoted");
                }

                Data.IsPrefered = false;
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(m => m.CustomerId).ToSaga(s => s.CustomerId);
        }

        public class State : ContainSagaData
        {
            [Unique]
            public virtual string CustomerId { get; set; }

            public virtual double YearlyRunningTotal { get; set; }

            public virtual bool IsPrefered { get; set; }
        }
    }
}