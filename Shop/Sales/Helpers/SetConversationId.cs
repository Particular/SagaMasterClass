namespace Sales.Helpers
{
    using Messages;
    using NServiceBus;
    using NServiceBus.MessageMutator;
    using NServiceBus.Unicast.Messages;

    class SetConversationId : IMutateOutgoingTransportMessages, INeedInitialization
    {
        public void MutateOutgoing(LogicalMessage logicalMessage, TransportMessage transportMessage)
        {
            var command = logicalMessage.Instance as IOrderCommand;

            if (command != null)
            {
                transportMessage.Headers[Headers.ConversationId] = command.OrderId;
            }
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(c => c.ConfigureComponent<SetConversationId>(DependencyLifecycle.SingleInstance));
        }
    }
}