using System.Collections;
using Health;
using UnityEngine;
using DG.Tweening;

namespace Enemies.UndeadMage
{
    public class UndeadMageProjectile : MonoBehaviour
    {
        [SerializeField] private LayerMask damageableLayer;
        private int _damage;
        private Vector2 _direction;
        private float _speed;
        private Rigidbody2D _rb;
        private Animator _animator;
        private bool _isDestroyed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                _rb = gameObject.AddComponent<Rigidbody2D>();
            }
            
            _rb.bodyType = RigidbodyType2D.Kinematic; 
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            _animator = GetComponent<Animator>();

            transform.localScale = Vector3.zero;
        }

        public void Setup(Vector2 direction, float speed, int damage, float startDelay)
        {
            _direction = direction.normalized;
            _speed = speed;
            _damage = damage;

            // DOTween bounce/pop effect
            transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
            
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (startDelay > 0f)
            {
                StartCoroutine(FireAfterDelay(startDelay));
            }
            else
            {
                _rb.linearVelocity = _direction * _speed;
            }
            
            Invoke(nameof(ForceDestroy), 7f);
        }

        private IEnumerator FireAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (!_isDestroyed)
            {
                _rb.linearVelocity = _direction * _speed;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_isDestroyed) return;

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
            
            TriggerDestroy();
        }

        private void TriggerDestroy()
        {
            _isDestroyed = true;
            _rb.linearVelocity = Vector2.zero; 
            
            if (_animator != null)
            {
                _animator.SetTrigger("destroy");
            }
            else
            {
                DestroySelf();
            }
        }

        private void ForceDestroy()
        {
            if (!_isDestroyed)
            {
                TriggerDestroy();
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill(); // Clean up tweens just in case to avoid memory leaks
        }
    }
}
