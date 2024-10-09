using UnityEngine;

namespace Enemies
{
    public abstract class EnemyProperties : ScriptableObject
    {
        public float WalkSpeed;
        public int BasicAttackPower;
        public float HitKnockBackPower;
        public float ChasePositionOffset;
        public float BlockChange;
        public float BlockDuration;
    }
}