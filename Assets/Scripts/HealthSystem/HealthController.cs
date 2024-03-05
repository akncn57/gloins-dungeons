using System;
using UnityEngine;

namespace HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        public long Health => _health;
        public long HealthLimit => _healthLimit;
        public long HealthOverLimit => _healthOverLimit;

        public event Action<long, long> OnHealthAdding;
        public event Action<long, long> OnHealthSpending;

        private readonly long _health;
        private readonly long _healthLimit;
        private readonly long _healthOverLimit;

        public HealthController(long health, long healthLimit, long healthOverLimit)
        {
            _health = health;
            _healthLimit = healthLimit;
            _healthOverLimit = healthOverLimit;
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

            if (_health > _healthLimit) return;

            var newHealth = _health + amount;
            OnHealthAdding?.Invoke(_health, newHealth);
        }

        public void SpendHealth(long amount)
        {
            if (amount < 0) Debug.LogError("Spending health amount can't be zero!");
            if (_health ! >= _healthOverLimit) return;

            var newHealth = _health - amount;
            OnHealthSpending?.Invoke(_health, newHealth);
        }
    }
}