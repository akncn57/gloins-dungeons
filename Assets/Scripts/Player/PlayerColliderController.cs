using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerColliderController : MonoBehaviour
    {
        public event Action OnHitStart;
        public event Action OnHitEnd;
        
        private Collider2D _collider;

        // private void Awake()
        // {
        //     if (TryGetComponent<Collider2D>(out var collider))
        //     {
        //         _collider = collider;
        //     }
        //     else
        //     {
        //         Debug.LogError("PlayerColliderController | Collider cannot founded!");
        //     }
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnHitStart?.Invoke();
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
