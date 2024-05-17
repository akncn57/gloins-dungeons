using DesignPatterns.ObserverPattern.CustomObservers;

namespace MiddleMans
{
    public class EnemyHealthHandler : IHealthObserver
    {
        public void OnHealthChanged(long tempHealth, long currentHealth)
        {
            throw new System.NotImplementedException();
        }

        public void OnHealthLimitChanged(long healthLimit)
        {
            throw new System.NotImplementedException();
        }
    }
}