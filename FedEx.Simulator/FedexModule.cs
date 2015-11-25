namespace FedEx.Simulator
{
    using Nancy;

    public class FedexModule : NancyModule
    {
        public FedexModule() : base("/fedex")
        {
            Get["/shipit"] = parameters =>
            {
                BehaviorHolder.Behavior.Simulate();
                return 201;
            };
        }
    }
}