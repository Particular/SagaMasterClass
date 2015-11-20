﻿namespace Shop.Sales
{
    using System;
    using System.Collections.Generic;
    using global::Sales.Messages;

    class StartOrderCommand : Command //<ShoppingCart>
    {
        public override void Execute(CommandContext context)
        {
            string currentOrderId;

            if (context.TryGet("CurrentOrderId", out currentOrderId))
            {
                Console.Out.WriteLine($"Order {currentOrderId} is currently active, please use PlaceOrder|CancelOrder to complete it first");

                return;
            }
            var orderId = Guid.NewGuid().ToString();

            context.Set("CurrentOrderId", orderId);
            context.Set("OrderItems", new List<OrderItem>());
            context.Status.Add($"{orderId.Substring(0, 5)}");
            context.Bus.Send(new StartOrder
            {
                OrderId = orderId
            });
            Console.Out.WriteLine($"Initiated order {orderId}, use AddItem <sku> <quantity> to buy things");
        }
    }

    class OrderItem
    {
    }
}