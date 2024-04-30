using System;
using ColliderController;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : ColliderControllerBase
    {
        public event Action<Vector3, int, float> PlayerOnHitStart;
        public event Action PlayerOnHitEnd;

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
                PlayerOnHitStart?.Invoke((transform.position - other.transform.position).normalized, 10, 5f);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerOnHitEnd?.Invoke();
            }
        }
    }
}
