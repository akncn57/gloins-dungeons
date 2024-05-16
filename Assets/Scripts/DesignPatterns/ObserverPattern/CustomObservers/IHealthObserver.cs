namespace DesignPatterns.ObserverPattern.CustomObservers
{
    public interface IHealthObserver
    {
        public void OnHealthChanged(long tempHealth, long currentHealth);
        public void OnHealthLimitChanged(long healthLimit);
    }
}