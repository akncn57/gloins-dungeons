using DesignPatterns.CommandPattern;

namespace Player.Commands
{
    public class PlayerStopMoveCommand : ICommand
    {
        public PlayerStopMoveCommand()
        {
             
        }
        
        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}