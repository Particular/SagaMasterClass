namespace Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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
}