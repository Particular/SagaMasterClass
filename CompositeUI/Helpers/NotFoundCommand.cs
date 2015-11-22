namespace Shop
{
    using System;

    class NotFoundCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Console.Out.WriteLine("Command not found");
        }
    }
}