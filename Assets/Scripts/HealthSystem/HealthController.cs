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
        public event Action<long> OnHealthLimitSet;

        private long _health;
        private long _healthLimit;
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

            var tempOldHealth = _health;
            _health += amount;
            OnHealthAdding?.Invoke(tempOldHealth, _health);
        }

        public void SpendHealth(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Spending health amount can't be zero!");
                return;
            }
            
            if (_health < 0)
            {
                Debug.LogError("Health already equal zero or less!");
                return;
            }

            var temOldHealth = _health;
            _health -= amount;
            OnHealthSpending?.Invoke(temOldHealth, _health);
        }

        public void SetHealthLimit(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Health limit must not less zero!");
                return;
            }

            _healthLimit = amount;
            OnHealthLimitSet?.Invoke(_healthLimit);
        }
    }
}