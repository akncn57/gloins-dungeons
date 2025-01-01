using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public abstract class EnemyProperties : ScriptableObject
    {
        public float WalkSpeed;
        public float RunSpeed;
        public int BasicAttackPower;
        public float HeavyAttackChance;
        public int HeavyAttackPower;
        public float HitKnockBackPower;
        public float ChaseRadius;
        public float ChasePositionOffset;
        public float BlockChance;
        public float BlockDuration;
    }
}