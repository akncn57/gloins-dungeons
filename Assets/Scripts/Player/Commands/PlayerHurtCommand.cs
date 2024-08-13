using DesignPatterns.CommandPattern;

namespace Player.Commands
{
    public class PlayerHurtCommand : ICommand
    {
        public PlayerHurtCommand()
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