using System;
using System.Collections;
using ColliderController;
using UnityEngine;

namespace Enemies.Mage
{
    public class MageProjectileController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        
        private readonly int _destroyAnimationParameter = Animator.StringToHash("Destroy");

        private void Start()
        {
            StartCoroutine(ProjectileLifeTimeCor(_lifeTime));
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.right * (_speed * Time.fixedDeltaTime) * -1;
        }

        private IEnumerator ProjectileLifeTimeCor(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
