using System;
using ColliderController;
using HitData;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : ColliderControllerBase
    {
        public event Action<int, Vector3, float> PlayerColliderOnHitStart;
        public event Action PlayerColliderOnHitEnd;
        
        [SerializeField] private PlayerStateMachine playerStateMachine;

        public override void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            base.InvokeOnHitStartEvent(damage, knockBackPosition, knockBackPower);
            playerStateMachine.HitData = new PlayerHitData(knockBackPosition, damage, knockBackPower);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerColliderOnHitStart?.Invoke(playerStateMachine.PlayerProperties.BasicAttackPower, (transform.position - other.transform.position).normalized, playerStateMachine.PlayerProperties.BasicAttackHitKnockBackPower);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerColliderOnHitEnd?.Invoke();
            }
        }
    }
}
