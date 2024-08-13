using DesignPatterns.CommandPattern;

namespace Player.Commands
{
    public class PlayerAttackBasicCommand : ICommand
    {
        public PlayerAttackBasicCommand()
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