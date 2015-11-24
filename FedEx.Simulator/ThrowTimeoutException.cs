namespace FedEx.Simulator
{
    using System;

    public class ThrowTimeoutException : FedexBehavior
    {
        public void Simulate()
        {
            throw new TimeoutException();
        }
    }
}