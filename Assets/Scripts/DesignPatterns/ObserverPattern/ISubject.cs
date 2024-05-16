namespace DesignPatterns.ObserverPattern
{
    public interface ISubject
    {
        public void RegisterObserver(IObserver observer);
        public void RemoveObserver(IObserver observer);
    }
}