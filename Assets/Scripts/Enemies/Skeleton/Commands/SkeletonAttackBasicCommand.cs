using DesignPatterns.CommandPattern;

namespace Enemies.Skeleton.Commands
{
    public class SkeletonAttackBasicCommand : ICommand
    {
        public SkeletonAttackBasicCommand()
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