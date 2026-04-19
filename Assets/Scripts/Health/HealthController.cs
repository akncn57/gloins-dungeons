using System;
using UnityEngine;

namespace Health
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public event Action<int, Vector2> OnTakeDamage;
        public event Action OnDeath;
        public event Action<int, int> OnHealthChanged; 
        
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        
        private bool _isDead;

        public void Initialize(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int damageTaken, Vector2 damageSourcePosition = default)
        {
            if (_isDead) return;
            CurrentHealth = Mathf.Max(CurrentHealth - damageTaken, 0);
            OnTakeDamage?.Invoke(CurrentHealth, damageSourcePosition);
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int healed)
        {
            if (_isDead) return;
            CurrentHealth = Mathf.Min(CurrentHealth + healed, MaxHealth);;
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}