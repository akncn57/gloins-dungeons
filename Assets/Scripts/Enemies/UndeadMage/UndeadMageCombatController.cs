using Health;
using UnityEngine;

namespace Enemies.UndeadMage
{
    public class UndeadMageCombatController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform attackPoint; 
        [SerializeField] private float heavyAttackRadius = 1.0f; 
        [SerializeField] private LayerMask damageableLayer;

        [Header("Projectile Settings")]
        [SerializeField] private Transform projectileSpawnPoint;
        
        private UndeadMageController _controller;

        private void Awake()
        {
            _controller = GetComponent<UndeadMageController>();
        }
        
        private void Update()
        {
            if (_controller != null && _controller.SpriteRenderer != null)
            {
                if (attackPoint != null)
                {
                    var absX = Mathf.Abs(attackPoint.localPosition.x);
                    var targetX = _controller.SpriteRenderer.flipX ? -absX : absX;
                    attackPoint.localPosition = new Vector3(targetX, attackPoint.localPosition.y, attackPoint.localPosition.z);
                }

                if (projectileSpawnPoint != null)
                {
                    var abspx = Mathf.Abs(projectileSpawnPoint.localPosition.x);
                    var targetpx = _controller.SpriteRenderer.flipX ? -abspx : abspx;
                    projectileSpawnPoint.localPosition = new Vector3(targetpx, projectileSpawnPoint.localPosition.y, projectileSpawnPoint.localPosition.z);
                }
            }
        }

        public void PerformMeleeAttack(int damageAmount)
        {
            if (attackPoint == null) return;
            
            var hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, heavyAttackRadius, damageableLayer);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    var damageable = hitCollider.GetComponentInParent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(damageAmount, transform.position);
                        break;
                    }
                }
            }
        }

        public void FireProjectile()
        {
            var stats = _controller.EnemyStats as UndeadMageStatsSO;
            if (stats == null || stats.ProjectilePrefab == null) return;

            var spawnPoint = projectileSpawnPoint != null ? projectileSpawnPoint.position : transform.position;
            Vector2 playerDir;
            if (_controller.PlayerTarget != null)
            {
                // Hedef oyuncunun merkezi olsun (biraz offset ekleyebiliriz y ekseninde gerekirse)
                Vector2 targetPos = _controller.PlayerTarget.position;
                targetPos.y += 0.5f; // oyuncunun merkezine doğru atması için
                playerDir = targetPos - (Vector2)spawnPoint;
            }
            else
            {
                playerDir = _controller.SpriteRenderer.flipX ? Vector2.left : Vector2.right;
            }

            var projGO = Instantiate(stats.ProjectilePrefab, spawnPoint, Quaternion.identity);
            if (projGO.TryGetComponent<UndeadMageProjectile>(out var projectile))
            {
                projectile.Setup(playerDir, stats.ProjectileSpeed, stats.LightAttackDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, heavyAttackRadius);
            }
        }
    }
}