namespace Sales
{
    using System;
    using Messages;
    using Shop;

    class CancelOrderCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            ShoppingCart cart;

            if (!context.TryGet(out cart))
            {
                Console.Out.WriteLine("No order is currently active, please use `StartOrder` to start a new one");
                return;
            }

            context.Bus.Send(new CancelOrder
            {
                OrderId = cart.OrderId
            });

            Console.Out.WriteLine($"Order {cart.OrderId} has been canceled");
            context.Remove<ShoppingCart>();
        }
    }
}