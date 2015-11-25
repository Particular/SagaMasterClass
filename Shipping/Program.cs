namespace Shipping
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
                .ConnectionString(@"Server=(localdb)\sagamasterclass;Database=Shipping;Trusted_Connection=True;");

            busConfiguration.EnableInstallers();

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Shipping endpoint is running, please hit any key to exit");
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