namespace FedEx.Simulator
{
    using System.Threading;
    using Shipping;

    public class TakeLonger : FedexBehavior
    {
        public void Simulate()
        {
            Thread.Sleep((FedEx.TimeoutInSeconds + 5)*1000);
        }
    }
}