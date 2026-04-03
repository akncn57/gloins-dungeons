
using System.Collections;
using Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyBase : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Components")] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Collider2D Collider { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Animator Animator { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public HealthController HealthController { get; private set; }
        
        [field: SerializeField, BoxGroup("Stats")] public EnemyStatsSO EnemyStats { get; set; }
        [field: SerializeField, BoxGroup("Settings")] private Material FlashMaterial { get; set; }
        
        [field: SerializeField] public Transform PlayerTarget { get; private set; }
        [field: SerializeField] public EnemyStateMachine StateMachine { get; protected set; }
        
        private Material _originalMaterial;
        private Coroutine _flashCoroutine;

        protected virtual void Awake()
        {
            PlayerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            _originalMaterial = SpriteRenderer.material;
        }
        
        protected virtual void Start()
        {
            HealthController.Initialize(EnemyStats.MaxHealth);

            HealthController.OnTakeDamage += HandleTakeDamage;
            HealthController.OnDeath += HandleDeath;
        }
        
        protected virtual void OnDestroy()
        {
            if (HealthController != null)
            {
                HealthController.OnTakeDamage -= HandleTakeDamage;
                HealthController.OnDeath -= HandleDeath;
            }
        }

        protected virtual void Update()
        {
            StateMachine.Update();
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
        
        protected virtual void HandleTakeDamage(int currentHealth, Vector2 damageSourcePosition)
        {
            Flash();
            StateMachine.ChangeState(StateMachine.HurtState);

            if (damageSourcePosition != Vector2.zero)
            {
                Vector2 knockbackDirection = ((Vector2)transform.position - damageSourcePosition).normalized;
                Rb.linearVelocity = Vector2.zero; // Clear current momentum
                Rb.AddForce(knockbackDirection * EnemyStats.KnockbackForce, ForceMode2D.Impulse);
            }
        }

        protected virtual void HandleDeath()
        {
            StateMachine.ChangeState(StateMachine.DeathState);
            Collider.enabled = false;
        }
        
        private void Flash()
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }
            
            _flashCoroutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            SpriteRenderer.material = FlashMaterial;
            yield return new WaitForSecondsRealtime(EnemyStats.HitFlashDuration);
            SpriteRenderer.material = _originalMaterial;
            
            _flashCoroutine = null;
        }

        private void OnDrawGizmosSelected()
        {
            if (EnemyStats != null)
            {
                // Pivot ayaklarda olduğu için, alanı görsel olarak (varsa) Collider'ın ortasına, 
                // ya da manuel olarak ayaklardan hafif yukarı çiziyoruz ki gövdeyi kaplasın.
                Vector3 center = Collider != null ? Collider.bounds.center : transform.position + Vector3.up * 1f;

                // Chase Area (Kovalamaca Alanı) - Sarı Çember
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(center, EnemyStats.ChaseRange);

                // Eğer statlar UndeadSwordsmanStatsSO ise özel menzillerini çiz
                if (EnemyStats is UndeadSwordsman.UndeadSwordsmanStatsSO swordsmanStats)
                {
                    // Saldırı Menzili (Kırmızı) - Kılıcın ucunun vuracağı maksimum yer
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(center, swordsmanStats.LightAttackRange);
                    
                    // Flank Distance (Mavi) - Düşmanın yaklaşmak için hedeflediği duruş noktası
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(center, swordsmanStats.FlankDistance);
                }
            }
        }
    }
}