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

            double orderValue = new Random().Next(100, 1000);
            var parts = context.GetParameters().Split(' ');
            if (parts.Length == 2)
            {
                orderValue = double.Parse(parts[1]);
            }
            context.Bus.Send(new PlaceOrder
            {
                OrderId = cart.OrderId,
                CustomerId = "12345abc",
                OrderDate = DateTime.UtcNow,
                OrderValue = orderValue
            });

            Console.Out.WriteLine($"Thank you for your order, your order confirmation should arrive shortly - {cart.OrderId}");
            context.Remove<ShoppingCart>();
        }
    }
}