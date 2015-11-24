namespace FedEx.Simulator
{
    using System.Threading;

    public class TakeLonger : FedexBehavior
    {
        public void Simulate()
        {
            Thread.Sleep(15000);
        }
    }
}