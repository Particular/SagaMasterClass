namespace Sales
{
    using System;
    using CustomerCare.Contracts;
    using NServiceBus;

    public class CustomerMadePreferedHandler : IHandleMessages<CustomerMadePrefered>
    {
        public void Handle(CustomerMadePrefered message)
        {
            Console.Out.WriteLine($"Customer {message.CustomerId} will get prefered customer discount");
        }
    }
}