using System;
using UnityEngine;

namespace HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        [field: SerializeField] public long Health { get; private set; }
        [field: SerializeField] public long HealthLimit { get; private set; }
        [field: SerializeField] public long HealthOverLimit { get; private set; }

        public event Action<long, long> OnHealthAdding;
        public event Action<long, long> OnHealthSpending;
        public event Action<long> OnHealthLimitSet;

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

            if (Health >= HealthLimit) return;

            var tempOldHealth = Health;
            Health += amount;
            OnHealthAdding?.Invoke(tempOldHealth, Health);
        }

        public void SpendHealth(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Spending health amount can't be zero!");
                return;
            }
            
            if (Health < 0)
            {
                Debug.LogError("Health already equal zero or less!");
                return;
            }

            var temOldHealth = Health;
            Health -= amount;
            OnHealthSpending?.Invoke(temOldHealth, Health);
        }

        public void SetHealthLimit(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Health limit must not less zero!");
                return;
            }

            HealthLimit = amount;
            OnHealthLimitSet?.Invoke(HealthLimit);
        }
    }
}