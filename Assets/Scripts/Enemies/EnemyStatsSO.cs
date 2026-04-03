using UnityEngine;

namespace Enemies
{
    public class EnemyStatsSO : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; } = 50;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float ChaseRange { get; private set; } = 8f;
    }
}