using Health;
using UnityEngine;

namespace Enemies.UndeadMage
{
    public class UndeadMageProjectile : MonoBehaviour
    {
        [SerializeField] private LayerMask damageableLayer;
        private int _damage;
        private Vector2 _direction;
        private float _speed;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                _rb = gameObject.AddComponent<Rigidbody2D>();
            }
            _rb.gravityScale = 0f;
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        public void Setup(Vector2 direction, float speed, int damage)
        {
            _direction = direction.normalized;
            _speed = speed;
            _damage = damage;
            
            _rb.linearVelocity = _direction * _speed;

            // Rotate projectile to face direction
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            // Destroy after 5 seconds to prevent memory leaks if it misses
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            // Check layer
            if ((damageableLayer.value & (1 << col.gameObject.layer)) == 0 && !col.CompareTag("Player")) 
                return;

            if (col.CompareTag("Player"))
            {
                var damageable = col.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_damage, transform.position);
                }
            }
            
            // Spawn hit effect here if needed

            Destroy(gameObject);
        }
    }
}
