using System;
using UnityEngine;

namespace LifeSystem
{
    public class LifeController : MonoBehaviour
    {
        public long Life => _life;
        public long LifeLimit => _lifeLimit;
        public long LifeOverLimit => _lifeOverLimit;

        public event Action<long, long> OnLifeAdding;
        public event Action<long, long> OnLifeSpending;

        private readonly long _life;
        private readonly long _lifeLimit;
        private readonly long _lifeOverLimit;

        public LifeController(long life, long lifeLimit, long lifeOverLimit)
        {
            _life = life;
            _lifeLimit = lifeLimit;
            _lifeOverLimit = lifeOverLimit;
        }
        
        public void AddLife(long amount)
        {
            switch (amount)
            {
                case 0:
                    Debug.LogError("Adding life amount can't be zero!");
                    break;
                case < 0:
                    Debug.LogError("Adding life amount can't be negative!");
                    break;
            }

            if (_life > _lifeLimit) return;

            var newLife = _life + amount;
            OnLifeAdding?.Invoke(_life, newLife);
        }

        public void SpendLife(long amount)
        {
            if (amount < 0) Debug.LogError("Spending life amount can't be zero!");
            if (_life ! >= _lifeOverLimit) return;

            var newLife = _life - amount;
            OnLifeSpending?.Invoke(_life, newLife);
        }
    }
}