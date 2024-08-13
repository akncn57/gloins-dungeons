using DesignPatterns.CommandPattern;

namespace Player.Commands
{
    public class PlayerFacingCommand : ICommand
    {
        public PlayerFacingCommand()
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