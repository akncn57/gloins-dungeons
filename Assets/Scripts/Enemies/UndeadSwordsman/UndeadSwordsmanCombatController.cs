using Health;
using UnityEngine;

namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanCombatController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform attackPoint; 
        [SerializeField] private float attackRadius = 1.5f; 
        [SerializeField] private LayerMask damageableLayer;
        
        private UndeadSwordsmanController _controller;

        private void Awake()
        {
            _controller = GetComponent<UndeadSwordsmanController>();
        }
        
        private void Update()
        {
            if (attackPoint != null && _controller != null && _controller.SpriteRenderer != null)
            {
                // Flip the attack point horizontally based on the sprite orientation
                var absX = Mathf.Abs(attackPoint.localPosition.x);
                var targetX = _controller.SpriteRenderer.flipX ? -absX : absX;
                
                attackPoint.localPosition = new Vector3(targetX, attackPoint.localPosition.y, attackPoint.localPosition.z);
            }
        }

        public void PerformMeleeAttack(int damageAmount)
        {
            Debug.Log($"<color=yellow>[Enemy Combat]</color> PerformMeleeAttack called with {damageAmount} damage.");
            
            if (attackPoint == null)
            {
                Debug.LogError("<color=yellow>[Enemy Combat]</color> AttackPoint is NULL! Please assign an Attack Point in the Inspector.");
                return;
            }
            
            var hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, damageableLayer);
            Debug.Log($"<color=yellow>[Enemy Combat]</color> OverlapCircleAll found {hitColliders.Length} colliders.");

            foreach (var hitCollider in hitColliders)
            {
                Debug.Log($"<color=yellow>[Enemy Combat]</color> Investigating collider: {hitCollider.name} (Tag: {hitCollider.tag})");
                
                if (hitCollider.CompareTag("Player"))
                {
                    var damageable = hitCollider.GetComponentInParent<IDamageable>();
                    
                    if (damageable != null)
                    {
                        damageable.TakeDamage(damageAmount, transform.position);
                        Debug.Log($"<color=green>[Enemy Combat]</color> UndeadSwordsman hit Player for {damageAmount} damage!");
                        break;
                    }
                    else
                    {
                        Debug.LogWarning($"<color=yellow>[Enemy Combat]</color> Player object {hitCollider.name} (or its parents) does NOT have IDamageable on it!");
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
            }
        }
    }
}
