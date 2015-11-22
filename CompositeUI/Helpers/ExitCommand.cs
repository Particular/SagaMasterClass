namespace Shop
{
    using System;

    class ExitCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Console.Out.WriteLine("bye bye");
        }
    }
}