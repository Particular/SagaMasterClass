namespace Sales
{
    using System;
    using NServiceBus;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;
    using NServiceBus.Logging;
    using NServiceBus.Persistence;

    class Program
    {
        static void Main()
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();

            busConfiguration.UsePersistence<NHibernatePersistence>()
                .ConnectionString(@"Server=.\sqlexpress;Database=Sales;Trusted_Connection=True;");

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Sales endpoint is running, please hit any key to exit");
                Console.ReadKey();
            }
        }
    }

    class ErrorQConfig : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}