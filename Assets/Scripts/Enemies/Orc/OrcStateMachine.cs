using System.Collections.Generic;
using Enemies.Orc.States;
using HealthSystem;
using HitData;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Zenject;

namespace Enemies.Orc
{
    public class OrcStateMachine : EnemyBaseStateMachine
    {
        [Inject] public IInstantiator Instantiator;

        [SerializeField] private OrcProperties _orcProperties;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask PlayerLayerMask;
        
        
        public bool HasLineOfSight => _enemyLineOfSight.HasLineOfSight(collider, PlayerCollider, "Player", PlayerLayerMask);
        public Collider2D PlayerCollider => playerCollider;

        public override EnemyProperties EnemyProperties => _orcProperties;
        public override HealthController HealthController { get; }
        public override EnemyColliderBaseController EnemyColliderController { get; }
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger { get; }
        public override NavMeshAgent EnemyNavMeshAgent => navMeshAgent;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CapsuleCollider2D AttackBasicCollider { get; }
        public override BoxCollider2D AttackHeavyCollider { get; }
        public override GameObject ParentObject { get; }
        public override GameObject ExclamationMarkObject { get; }
        public override GameObject WarningObject { get; }
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle { get; }
        public override ParticleSystem BlockEffectParticle { get; }
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override EnemyFacing EnemyFacing { get; }
        public override EnemyLineOfSight EnemyLineOfSight => _enemyLineOfSight;
        public override EnemySetDestination EnemySetDestination => _enemySetDestination;
        public override EnemyStopMovement EnemyStopMovement => _enemyStopMovement;
        public override EnemyStopRigidbody EnemyStopRigidbody => _enemyStopRigidbody;
        public override EnemyKnockback EnemyKnockback { get; }
        public override EnemyDrawChaseOverlay EnemyDrawChaseOverlay => _enemyDrawChaseOverlay;

        public OrcIdleState OrcIdleState { get; private set; }
        public OrcPatrolState OrcPatrolState { get; private set; }
        public OrcChaseState OrcChaseState { get; private set; }
        public OrcBasicAttackState OrcBasicAttackState { get; private set; }
        
        private EnemySetDestination _enemySetDestination;
        private EnemyStopMovement _enemyStopMovement;
        private EnemyStopRigidbody _enemyStopRigidbody;
        private EnemyDrawChaseOverlay _enemyDrawChaseOverlay;
        private EnemyLineOfSight _enemyLineOfSight;

        private void Awake()
        {
            OrcIdleState = Instantiator.Instantiate<OrcIdleState>(new object[]{this});
            OrcPatrolState = Instantiator.Instantiate<OrcPatrolState>(new object[]{this});
            OrcChaseState = Instantiator.Instantiate<OrcChaseState>(new object[] { this });
            OrcBasicAttackState = Instantiator.Instantiate<OrcBasicAttackState>(new object[] { this });
            
            _enemySetDestination = new EnemySetDestination();
            _enemyStopMovement = new EnemyStopMovement();
            _enemyStopRigidbody = new EnemyStopRigidbody();
            _enemyDrawChaseOverlay = new EnemyDrawChaseOverlay();
            _enemyLineOfSight = new EnemyLineOfSight();
            
            navMeshAgent.speed = EnemyProperties.WalkSpeed;
        }
        
        private void Start()
        {
            SwitchState(OrcIdleState);
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