namespace CustomerCare.Contracts
{
    using NServiceBus;

    public class CustomerDemotedToRegularStatus : IEvent
    {
        public string CustomerId { get; set; }
    }
}