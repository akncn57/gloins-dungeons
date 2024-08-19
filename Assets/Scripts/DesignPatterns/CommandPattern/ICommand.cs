namespace DesignPatterns.CommandPattern
{
    public interface ICommand
    {
        public object Execute();
        public void Undo();
    }
}