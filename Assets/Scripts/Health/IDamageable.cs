using UnityEngine;

namespace Health
{
    public interface IDamageable
    {
        public void TakeDamage(int damage, Vector2 damageSourcePosition = default);
    }
}