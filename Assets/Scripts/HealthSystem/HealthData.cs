using DesignPatterns.ObserverPattern;

namespace HealthSystem
{
    public sealed class HealthData : ISubject
    {
        public long Health;
        public long HealthLimit;
        
        public void RegisterObserver(IObserver observer)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveObserver(IObserver observer)
        {
            throw new System.NotImplementedException();
        }
    }
}