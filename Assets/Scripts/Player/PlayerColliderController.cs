using System;
using ColliderController;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : ColliderControllerBase
    {
        public event Action<int, Vector3, float> PlayerColliderOnHitStart;
        public event Action PlayerColliderOnHitEnd;

        public override void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            base.InvokeOnHitStartEvent(damage, knockBackPosition, knockBackPower);
        }

        public override void InvokeOnHitEndEvent()
        {
            base.InvokeOnHitEndEvent();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerColliderOnHitStart?.Invoke(10, (transform.position - other.transform.position).normalized, 5f);
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
