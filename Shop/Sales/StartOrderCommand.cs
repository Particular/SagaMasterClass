namespace Sales
{
    using System;
    using Messages;
    using Shop;

    class StartOrderCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            ShoppingCart currentCart;

            if (context.TryGet(out currentCart))
            {
                Console.Out.WriteLine($"Order {currentCart.OrderId} is currently active, please use PlaceOrder|CancelOrder to complete it first");

                return;
            }
            var orderId = Guid.NewGuid().ToString();

            context.Set(new ShoppingCart(orderId));
            context.Bus.Send(new StartOrder
            {
                OrderId = orderId
            });
            Console.Out.WriteLine($"Order {orderId} created, use PlaceOrder|CancelOrder to proceed");
        }
    }
}