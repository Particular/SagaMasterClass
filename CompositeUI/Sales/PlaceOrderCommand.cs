namespace Sales
{
    using System;
    using Messages;
    using Shop;

    class PlaceOrderCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            string orderId;

            if (!context.TryGet("CurrentOrderId", out orderId))
            {
                Console.Out.WriteLine("No order is currently active, please use `NewOrder` to start a new one");

                return;
            }

            context.Bus.Send(new PlaceOrder
            {
                OrderId = orderId
            });

            Console.Out.WriteLine($"Thank you for your order, your order confirmation should arrive shortly - {orderId}");
            context.Remove("CurrentOrderId");
            context.Status.Clear();
        }
    }
}