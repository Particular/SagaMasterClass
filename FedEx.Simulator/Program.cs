namespace FedEx.Simulator
{
    using System;
    using Nancy.Hosting.Self;

    class Program
    {
        static void Main(string[] args)
        {
            var behaviors = new FedexBehavior[]
            {
                new Success(),
                new ThrowTimeoutException(),
                new Success(),
                new TakeLonger(),  
            };

            BehaviorHolder.Behavior = behaviors[0];

            using (var host = new NancyHost(new Uri("http://localhost:8888")))
            {
                host.Start();
                Console.WriteLine("What behavior would you like?");
                Console.WriteLine("[1] TimeoutException");
                Console.WriteLine("[2] Success (default)");
                Console.WriteLine("[3] Take longer than logical timeout");
                Console.WriteLine();
                Console.WriteLine("Please press 'q' to exit.");

                string key;
                do
                {
                    key = Console.ReadKey().KeyChar.ToString().ToLowerInvariant();
                    int index;
                    if (int.TryParse(key, out index))
                    {
                        BehaviorHolder.Behavior = behaviors[index];
                    }
                } while (key != "q");
            }
        }
    }
}
