namespace DesignPatterns.CommandPattern
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}