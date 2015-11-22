namespace Sales
{
    using System;
    using Messages;
    using Shop;

    class PlaceOrderCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            ShoppingCart cart;

            if (!context.TryGet(out cart))
            {
                Console.Out.WriteLine("No order is currently active, please use `StartOrder` to start a new one");
                return;
            }

            context.Bus.Send(new PlaceOrder
            {
                OrderId = cart.OrderId
            });

            Console.Out.WriteLine($"Thank you for your order, your order confirmation should arrive shortly - {cart.OrderId}");
            context.Remove<ShoppingCart>();
        }
    }
}