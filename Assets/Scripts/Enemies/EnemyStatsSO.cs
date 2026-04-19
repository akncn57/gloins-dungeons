using UnityEngine;

namespace Enemies
{
    public class EnemyStatsSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; } = 50;
        [field: SerializeField] public float KnockbackForce { get; private set; } = 5f;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float ChaseRange { get; private set; } = 8f;
        [field: SerializeField] public float HitFlashDuration { get; private set; } = 0.5f;
    }
}