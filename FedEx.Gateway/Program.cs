namespace FedEx.Gateway
{
    using System;
    using NServiceBus;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;
    using NServiceBus.Logging;

    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            busConfiguration.EnableInstallers();

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Fedex endpoint is running, please hit any key to exit");
                Console.ReadKey();
            }
        }

        class CustomConfig : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>,
            IProvideConfiguration<AuditConfig>,
            IProvideConfiguration<TransportConfig>
        {
            AuditConfig IProvideConfiguration<AuditConfig>.GetConfiguration()
            {
                return new AuditConfig
                {
                    QueueName = "audit"
                };
            }

            public MessageForwardingInCaseOfFaultConfig GetConfiguration()
            {
                return new MessageForwardingInCaseOfFaultConfig
                {
                    ErrorQueue = "error"
                };
            }

            TransportConfig IProvideConfiguration<TransportConfig>.GetConfiguration()
            {
                return new TransportConfig
                {
                    MaximumConcurrencyLevel = 10,
                    MaximumMessageThroughputPerSecond = 0
                };
            }
        }
    }
}
