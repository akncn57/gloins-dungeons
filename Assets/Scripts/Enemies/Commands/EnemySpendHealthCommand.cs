using DesignPatterns.CommandPattern;

namespace Enemies.Skeleton.Commands
{
    public class EnemySpendHealthCommand : ICommand
    {
        public EnemySpendHealthCommand()
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