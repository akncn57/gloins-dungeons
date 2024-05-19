using UnityEngine;

namespace HealthSystem
{
    public class HealthController
    {
        public HealthData HealthData => _healthData;

        private readonly HealthData _healthData;

        public HealthController(long health, long healthLimit)
        {
            _healthData = new HealthData(health, healthLimit);
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
        }

        public void SetHealthLimit(long amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Health limit must not less zero!");
                return;
            }

            _healthData.HealthLimit = amount;
        }
    }
}