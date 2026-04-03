
using Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyBase : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Components")] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Collider2D Collider { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Animator Animator { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public HealthController HealthController { get; private set; }
        
        [field: SerializeField, BoxGroup("Stats")] public EnemyStatsSO EnemyStats { get; set; }
        
        [field: SerializeField] public Transform PlayerTarget { get; private set; }
        [field: SerializeField] public EnemyStateMachine StateMachine { get; protected set; }

        protected virtual void Awake()
        {
            PlayerTarget = GameObject.FindGameObjectWithTag("Player").transform;
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