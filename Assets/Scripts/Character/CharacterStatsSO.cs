using Sirenix.OdinInspector;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Game Data/Character Stats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [Title("Movement Settings", "Basic movement settings")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

        [Title("Attack Settings", "Attack and cooldown settings")]
        [field: SerializeField] public int HeavyAttackCooldown { get; private set; } = 3;

        [Title("Dash Settings", "Dash ability settings")]
        [field: SerializeField] public float DashSpeed { get; private set; } = 10f;
        [field: SerializeField] public float DashDuration { get; private set; } = 0.2f;
        [field: SerializeField] public float DashCooldown { get; set; } = 3;
    }
}