namespace Shop.Sales
{
    using System;
    using System.Collections.Generic;

    class NewOrderCommand : Command
    {
        public override void Execute(CommandContext commandContext)
        {
            string currentOrderId;

            if (commandContext.TryGet("CurrentOrderId", out currentOrderId))
            {
                Console.Out.WriteLine($"Order {currentOrderId} is currently active, please use PlaceOrder|CancelOrder to complete it first");

                return;
            }
            var orderId = Guid.NewGuid().ToString();

            commandContext.Set("CurrentOrderId", orderId);
            commandContext.Set("OrderItems", new List<OrderItem>());
            commandContext.Status.Add($"{orderId.Substring(0, 5)}");

            Console.Out.WriteLine($"Initiated order {orderId}, use AddItem <sku> <quantity> to buy things");
        }
    }

    class OrderItem
    {
    }
}