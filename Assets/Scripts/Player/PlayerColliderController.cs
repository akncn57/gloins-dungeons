using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : MonoBehaviour
    {
        public event Action<Vector3, float> OnHitStart;
        public event Action OnHitEnd;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnHitStart?.Invoke((transform.position - other.transform.position).normalized, 5f);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnHitEnd?.Invoke();
            }
        }
    }
}
