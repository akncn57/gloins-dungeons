using UnityEngine;

namespace Enemies.UndeadMage
{
    [CreateAssetMenu(fileName = "NewUndeadMageStats", menuName = "Game Data/Undead Mage Stats")]
    public class UndeadMageStatsSO : EnemyStatsSO
    {
        [field: Header("Attack Settings")]
        [field: SerializeField] public float LightAttackRange { get; private set; } = 4.0f;
        [field: SerializeField] public float RetreatDistance { get; private set; } = 2.0f;
        [field: SerializeField] public float HeavyAttackRange { get; private set; } = 1.0f;
        
        [field: Header("Damage Settings")]
        [field: SerializeField] public int LightAttackDamage { get; private set; } = 15;
        [field: SerializeField] public int HeavyAttackDamage { get; private set; } = 25;
        
        [field: Header("Cooldowns")]
        [field: SerializeField] public float LightAttackCooldown { get; private set; } = 3f;
        [field: SerializeField] public float HeavyAttackCooldown { get; private set; } = 2f;
        
        [field: Header("Projectile Settings")]
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; } = 5f;
    }
}