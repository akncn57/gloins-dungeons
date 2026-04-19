using UnityEngine;

namespace Enemies.UndeadSwordsman
{
    [CreateAssetMenu(fileName = "NewUndeadSwordsmanStats", menuName = "Game Data/Undead Swordsman Stats")]
    public class UndeadSwordsmanStatsSO : EnemyStatsSO
    {
        [field: SerializeField] public float LightAttackRange { get; private set; } = 1.5f;
        [field: SerializeField] public float FlankDistance { get; private set; } = 1.0f;
        [field: SerializeField] public int LightAttackDamage { get; private set; } = 10;
        [field: SerializeField] public int HeavyAttackDamage { get; private set; } = 10;
        [field: SerializeField] public float LightAttackAttackCooldown { get; private set; } = 2f;
        [field: SerializeField] public float HeavyAttackAttackCooldown { get; private set; } = 2f;
        
        [field: Header("Attack Chances")]
        [field: Tooltip("Zamanı geldiğinde Heavy Attack yapma şansı (0 = Hiç, 1 = Her zaman)")]
        [field: SerializeField, Range(0f, 1f)] public float HeavyAttackChance { get; private set; } = 0.2f;
    }
}