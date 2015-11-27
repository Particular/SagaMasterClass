namespace Sales
{
    using System;
    using CustomerCare.Contracts;
    using NServiceBus;

    public class CustomerDemotedHandler : IHandleMessages<CustomerDemotedToRegularStatus>
    {
        public void Handle(CustomerDemotedToRegularStatus message)
        {
            Console.Out.WriteLine($"Customer {message.CustomerId} is removed from the prefered customer discount");
        }
    }
}