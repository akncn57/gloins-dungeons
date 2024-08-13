using System.Collections.Generic;

namespace DesignPatterns.CommandPattern
{
    public class CommandInvoker
    {
        private static Stack<ICommand> _undoStack = new Stack<ICommand>();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
        }
    }
}