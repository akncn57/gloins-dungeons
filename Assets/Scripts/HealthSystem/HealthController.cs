using System.Collections.Generic;
using DesignPatterns.ObserverPattern.CustomObservers;
using UnityEngine;

namespace HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        public HealthData HealthData => _healthData;
        
        private readonly HealthData _healthData = new();
        private readonly List<IHealthObserver> _observers = new();
        
        public void AddObserver(IHealthObserver observer)
        {
            _observers.Add(observer);
        }
        
        public void RemoveObserver(IHealthObserver observer)
        {
            _observers.Remove(observer);
        }

        public void AddHealth(long amount)
        {
            switch (amount)
            {
                case 0:
                    Debug.LogError("Adding health amount can't be zero!");
                    break;
                case < 0:
                    Debug.LogError("Adding health amount can't be negative!");
                    break;
            }

            if (_healthData.Health >= _healthData.HealthLimit) return;

            var tempOldHealth = _healthData.Health;
            _healthData.Health += amount;
            
            foreach (var observer in _observers)
            {
                observer.OnHealthChanged(tempOldHealth, _healthData.Health);
            }
        }

        public void SpendHealth(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Spending health amount can't be zero!");
                return;
            }
            
            if (_healthData.Health < 0)
            {
                Debug.LogError("Health already equal zero or less!");
                return;
            }

            var temOldHealth = _healthData.Health;
            _healthData.Health -= amount;
            
            foreach (var observer in _observers)
            {
                observer.OnHealthChanged(temOldHealth, _healthData.Health);
            }
        }

        public void SetHealthLimit(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Health limit must not less zero!");
                return;
            }

            _healthData.HealthLimit = amount;
            
            foreach (var observer in _observers)
            {
                observer.OnHealthLimitChanged(_healthData.HealthLimit);
            }
        }
    }
}