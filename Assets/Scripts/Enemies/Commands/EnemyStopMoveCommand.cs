using DesignPatterns.CommandPattern;

namespace Enemies.Skeleton.Commands
{
    public class EnemyStopMoveCommand : ICommand
    {
        public EnemyStopMoveCommand()
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