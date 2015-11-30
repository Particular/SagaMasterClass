namespace Shop
{
    using System;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static void Main()
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();

            using (var bus = Bus.CreateSendOnly(busConfiguration))
            {
                var commandContext = new CommandContext(bus);

                Console.Out.WriteLine("Welcome to the Acme, please start a new order using `StartOrder`. Type `exit` to exit");
                RunCommandLoop(commandContext);
            }
        }

        static void RunCommandLoop(CommandContext context)
        {
            Command command;

            do
            {
                GeneratePrompt(context);
                var requestedCommand = Console.ReadLine();

                command = Command.Parse(requestedCommand);

                context.SetParameters(requestedCommand);
                try
                {
                    command.Execute(context);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to understand input");
                }
            } while (!(command is ExitCommand));
        }

        static void GeneratePrompt(CommandContext context)
        {
            ShoppingCart cart;

            string promptContext = null;
            if (context.TryGet(out cart))
            {
                promptContext = cart.OrderId.Substring(0, 6);
            }

            if (!string.IsNullOrEmpty(promptContext))
            {
                promptContext = $" [{promptContext}]";
            }

            Console.Out.Write($"Shop{promptContext}>");
        }
    }
}