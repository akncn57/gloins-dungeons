using DesignPatterns.ObserverPattern.CustomObservers;
using EventInterfaces;
using Zenject;

namespace MiddleMans
{
    public class PlayerHealthHandler : IHealthObserver
    {
        private SignalBus _signalBus;
        
        public void OnHealthChanged(long tempHealth, long currentHealth)
        {
            _signalBus.Fire(new IPlayerEvents.OnPlayerHealthChanged
            {
                TempHealth = tempHealth,
                CurrentHealth = currentHealth
            });
        }

        public void OnHealthLimitChanged(long healthLimit)
        {
            _signalBus.Fire(new IPlayerEvents.OnPlayerHealthLimitChanged()
            {
                CurrentHealthLimit = healthLimit
            });
        }
    }
}