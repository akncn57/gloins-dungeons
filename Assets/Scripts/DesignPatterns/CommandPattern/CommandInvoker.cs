using System.Collections.Generic;

namespace DesignPatterns.CommandPattern
{
    public class CommandInvoker
    {
        private static readonly Stack<ICommand> UndoStack = new Stack<ICommand>();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            UndoStack.Push(command);
        }
        
        public static void UndoCommand()
        {
            if (UndoStack.Count <= 0) return;
            
            var activeCommand = UndoStack.Pop();
            activeCommand.Undo();
        }
    }
}