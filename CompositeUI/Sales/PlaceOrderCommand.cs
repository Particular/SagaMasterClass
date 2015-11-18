namespace Sales
{
    using System;
    using Messages;
    using Shop;

    class PlaceOrderCommand : Command
    {
        public override void Execute(CommandContext commandContext)
        {
            string orderId;

            if (!commandContext.TryGet("CurrentOrderId", out orderId))
            {
                Console.Out.WriteLine("No order is currently active, please use `NewOrder` to start a new one");

                return;
            }

            commandContext.Bus.Send(new PlaceOrder
            {
                OrderId = orderId
            });

            Console.Out.WriteLine($"Thank you for your order, your order confirmation should arrive shortly - {orderId}");
            commandContext.Remove("CurrentOrderId");
            commandContext.Status.Clear();
        }
    }
}