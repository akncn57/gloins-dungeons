namespace DesignPatterns.ObserverPattern.CustomObservers
{
    public interface IHealthObserver : IObserver
    {
        public void OnHealthChanged(long tempHealth, long currentHealth);
        public void OnHealthLimitChanged(long healthLimit);
    }
}