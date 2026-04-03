
using Health;
using UnityEngine;

namespace Enemies
{
    public class EnemyBase : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public HealthController HealthController { get; private set; }
        
        [field: SerializeField] public Transform PlayerTarget { get; private set; }
        [field: SerializeField] public EnemyStateMachine StateMachine { get; protected set; }

        protected virtual void Awake()
        {
            PlayerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        protected virtual void Start()
        {
            // Health.Initialize(Stats.MaxHealth);

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
        
        protected virtual void HandleTakeDamage(int currentHealth)
        {
            StateMachine.ChangeState(StateMachine.HurtState);
        }

        protected virtual void HandleDeath()
        {
            StateMachine.ChangeState(StateMachine.DeathState);
            Collider.enabled = false;
        }
    }
}