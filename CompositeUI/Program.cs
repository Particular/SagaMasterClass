﻿namespace Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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

        static void RunCommandLoop(CommandContext commandContext)
        {
            Command command;

            do
            {
                GeneratePrompt(commandContext);
                var requestedCommand = Console.ReadLine();

                command = Command.Parse(requestedCommand);


                command.Execute(commandContext);
            } while (!(command is ExitCommand));
        }

        static void GeneratePrompt(CommandContext commandContext)
        {
            var promptContext = string.Join(" ", commandContext.Status);

            if (!string.IsNullOrEmpty(promptContext))
            {
                promptContext = $" [{promptContext}]";
            }

            Console.Out.Write($"Shop{promptContext}>");
        }
    }


    abstract class Command
    {
        static Command()
        {
            availableCommands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Command).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();
        }

        public static Command Parse(string commandline)
        {
            var parts = commandline?.Split(' ');
            if (parts == null || !parts.Any())
            {
                return new NotFoundCommand();
            }

            var command = availableCommands.FirstOrDefault(t => t.Name.ToLower()
                .StartsWith(parts.First().ToLower()));
            if (command == null)
            {
                return new NotFoundCommand();
            }

            return (Command) Activator.CreateInstance(command);
        }

        public abstract void Execute(CommandContext context);

        static List<Type> availableCommands;
    }

    class ExitCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Console.Out.WriteLine("bye bye");
        }
    }

    class NotFoundCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Console.Out.WriteLine("Command not found");
        }
    }
}