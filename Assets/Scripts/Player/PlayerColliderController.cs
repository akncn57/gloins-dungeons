using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : MonoBehaviour
    {
        public event Action<Vector3> OnHitStart;
        public event Action OnHitEnd;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Hit Position : " + other.gameObject.transform.position);
                OnHitStart?.Invoke(other.gameObject.transform.position);
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
