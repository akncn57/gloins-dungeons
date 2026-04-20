using UnityEngine;

namespace Health
{
    public enum AttackType
    {
        General,
        Light,
        Heavy
    }

    public interface IDamageable
    {
        public void TakeDamage(int damage, Vector2 damageSourcePosition = default, AttackType attackType = AttackType.General);
    }
}