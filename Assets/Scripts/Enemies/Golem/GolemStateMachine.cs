using System.Collections.Generic;
using Enemies.Golem.States;
using Enemies.Orc;
using Enemies.Orc.States;
using HealthSystem;
using HitData;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Zenject;

namespace Enemies.Golem
{
    public class GolemStateMachine : EnemyBaseStateMachine
    {
        [Inject] public IInstantiator Instantiator;

        [SerializeField] private GolemProperties _golemProperties;
        [SerializeField] private GolemColliderController _golemColliderController;
        [SerializeField] private GolemAnimationEventTrigger _golemAnimationEventTrigger;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider;
        [SerializeField] private Collider2D basicAttackColliderUp;
        [SerializeField] private Collider2D basicAttackColliderDown;
        [SerializeField] private Collider2D basicAttackColliderLeft;
        [SerializeField] private Collider2D basicAttackColliderRight;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private ParticleSystem _bloodParticle;
        
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask PlayerLayerMask;
        
        
        public bool HasLineOfSight => _enemyLineOfSight.HasLineOfSight(collider, PlayerCollider, "Player", PlayerLayerMask);
        public Collider2D PlayerCollider => playerCollider;
        public LayerMask PlayerLayer => PlayerLayerMask;

        public override EnemyProperties EnemyProperties => _golemProperties;
        public override HealthController HealthController => _healthController;
        public override EnemyColliderBaseController EnemyColliderController => _golemColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => _golemAnimationEventTrigger;
        public override NavMeshAgent EnemyNavMeshAgent => navMeshAgent;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override Collider2D BasicAttackColliderUp => basicAttackColliderUp;
        public override Collider2D BasicAttackColliderDown => basicAttackColliderDown;
        public override Collider2D BasicAttackColliderLeft => basicAttackColliderLeft;
        public override Collider2D BasicAttackColliderRight => basicAttackColliderRight;
        public override BoxCollider2D AttackHeavyCollider { get; }
        public override GameObject ParentObject { get; }
        public override GameObject ExclamationMarkObject { get; }
        public override GameObject WarningObject { get; }
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => _bloodParticle;
        public override ParticleSystem BlockEffectParticle { get; }
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override EnemyFacing EnemyFacing { get; }
        public override EnemyLineOfSight EnemyLineOfSight => _enemyLineOfSight;
        public override EnemySetDestination EnemySetDestination => _enemySetDestination;
        public override EnemyStopMovement EnemyStopMovement => _enemyStopMovement;
        public override EnemyStopRigidbody EnemyStopRigidbody => _enemyStopRigidbody;
        public override EnemyKnockback EnemyKnockback => _enemyKnockback;
        public override EnemyDrawChaseOverlay EnemyDrawChaseOverlay => _enemyDrawChaseOverlay;

        public GolemIdleState GolemIdleState { get; private set; }
        public GolemPatrolState GolemPatrolState { get; private set; }
        public GolemChaseState GolemChaseState { get; private set; }
        public GolemBasicAttackState GolemBasicAttackState { get; private set; }
        public GolemHurtState GolemHurtState { get; private set; }
        public GolemDeathState GolemDeathState { get; private set; }

        private HealthController _healthController;
        private EnemySetDestination _enemySetDestination;
        private EnemyStopMovement _enemyStopMovement;
        private EnemyStopRigidbody _enemyStopRigidbody;
        private EnemyKnockback _enemyKnockback;
        private EnemyDrawChaseOverlay _enemyDrawChaseOverlay;
        private EnemyLineOfSight _enemyLineOfSight;

        private void Awake()
        {
            GolemIdleState = Instantiator.Instantiate<GolemIdleState>(new object[]{this});
            GolemPatrolState = Instantiator.Instantiate<GolemPatrolState>(new object[]{this});
            GolemChaseState = Instantiator.Instantiate<GolemChaseState>(new object[] { this });
            GolemBasicAttackState = Instantiator.Instantiate<GolemBasicAttackState>(new object[] { this });
            GolemHurtState = Instantiator.Instantiate<GolemHurtState>(new object[] { this });
            GolemDeathState = Instantiator.Instantiate<GolemDeathState>(new object[] { this });

            _healthController = new HealthController(EnemyProperties.Health, EnemyProperties.Health);
            
            _enemySetDestination = new EnemySetDestination();
            _enemyStopMovement = new EnemyStopMovement();
            _enemyStopRigidbody = new EnemyStopRigidbody();
            _enemyKnockback = new EnemyKnockback();
            _enemyDrawChaseOverlay = new EnemyDrawChaseOverlay();
            _enemyLineOfSight = new EnemyLineOfSight();
            
            navMeshAgent.speed = EnemyProperties.WalkSpeed;
        }
        
        private void Start()
        {
            SwitchState(GolemIdleState);
        }
        
        public void ResetPatrolCoordinateStatus()
        {
            foreach (var coordinate in PatrolCoordinates)
            {
                coordinate.IsCompleted = false;
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, EnemyProperties.ChaseRadius);
        }
#endif
    }
}