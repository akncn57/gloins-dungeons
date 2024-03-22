using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : MonoBehaviour
    {
        public event Action<Vector3, int, float> PlayerOnHitStart;
        public event Action PlayerOnHitEnd;
        
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
