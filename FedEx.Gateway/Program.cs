namespace FedEx.Gateway
{
    using System;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();

            busConfiguration.EnableInstallers();

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Fedex endpoint is running, please hit any key to exit");
                Console.ReadKey();
            }
        }
    }
}
