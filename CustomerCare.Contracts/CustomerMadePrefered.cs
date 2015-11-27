namespace CustomerCare.Contracts
{
    using NServiceBus;

    public class CustomerMadePrefered : IEvent
    {
        public string CustomerId { get; set; }
    }
}