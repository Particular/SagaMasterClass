namespace FedEx.Simulator
{
    using System;
    using System.Diagnostics;
    using Nancy.Hosting.Self;
    using Shipping;

    class Program
    {
        static void Main(string[] args)
        {
            var behaviors = new FedexBehavior[]
            {
                new Success(),
                new ThrowTimeoutException(),
                new Success(),
                new TakeLonger()
            };

            BehaviorHolder.Behavior = behaviors[0];

            NetAclChecker.AddAddress("http://+:8888/");

            using (var host = new NancyHost(new Uri("http://localhost:8888")))
            {
                host.Start();
                Console.WriteLine("What behavior would you like?");
                Console.WriteLine("[1] TimeoutException");
                Console.WriteLine("[2] Success (default)");
                Console.WriteLine($"[3] Delay with {FedEx.TimeoutInSeconds + 5} seconds");
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
                        Console.Out.WriteLine($"{BehaviorHolder.Behavior.GetType().Name} selected");
                    }
                } while (key != "q");
            }
        }
    }

    public static class NetAclChecker
    {
        public static void AddAddress(string address)
        {
            AddAddress(address, Environment.UserDomainName, Environment.UserName);
        }

        public static void AddAddress(string address, string domain, string user)
        {
            var args = $@"http add urlacl url={address} user={domain}\{user}";

            var psi = new ProcessStartInfo("netsh", args);
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true;

            Process.Start(psi)?.WaitForExit();
        }
    }
}