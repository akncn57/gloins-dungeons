using System;
using System.Collections.Generic;
using Enemies.Skeleton.States;
using HealthSystem;
using HitData;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonStateMachine : EnemyBaseStateMachine
    {
        [SerializeField] private EnemyProperties skeletonProperties;
        [SerializeField] private SkeletonColliderController skeletonColliderController;
        [SerializeField] private SkeletonAnimationEventTrigger skeletonAnimationEventTrigger;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Collider2D collider;
        [SerializeField] private CapsuleCollider2D attackBasicCollider;
        [SerializeField] private BoxCollider2D attackHeavyCollider;
        [SerializeField] private GameObject parentObject;
        [SerializeField] private GameObject exclamationMarkObject;
        [SerializeField] private GameObject warningObject;
        [SerializeField] private CircleCollider2D chaseCollider;
        [SerializeField] private ParticleSystem hurtParticle;
        [SerializeField] private ParticleSystem blockEffectParticle;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private Collider2D PlayerCollider;
        [SerializeField] private LayerMask PlayerLayerMask;
        
        private HealthController _healthController;
        private EnemyFacing _enemyFacing;
        private EnemyFindClosestChasePoint _enemyFindClosestChasePoint;
        private EnemyLineOfSight _enemyLineOfSight;
        private EnemySetDestination _enemySetDestination;
        private EnemyStopMovement _enemyStopMovement;
        private EnemyKnockback _enemyKnockback;
        private SkeletonDrawChaseOverlay _skeletonDrawChaseOverlay;

        public override EnemyProperties EnemyProperties => skeletonProperties;
        public override HealthController HealthController => _healthController;
        public override EnemyColliderBaseController EnemyColliderController => skeletonColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => skeletonAnimationEventTrigger;
        public override NavMeshAgent EnemyNavMeshAgent => navMeshAgent;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CapsuleCollider2D AttackBasicCollider => attackBasicCollider;
        public override BoxCollider2D AttackHeavyCollider => attackHeavyCollider;
        public override GameObject ParentObject => parentObject;
        public override GameObject ExclamationMarkObject => exclamationMarkObject;
        public override GameObject WarningObject => warningObject;
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => hurtParticle;
        public override ParticleSystem BlockEffectParticle => blockEffectParticle;
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override EnemyFacing EnemyFacing => _enemyFacing;
        public override EnemyFindClosestChasePoint EnemyFindClosestChasePoint => _enemyFindClosestChasePoint;
        public override EnemyLineOfSight EnemyLineOfSight => _enemyLineOfSight;
        public override EnemySetDestination EnemySetDestination => _enemySetDestination;
        public override EnemyStopMovement EnemyStopMovement => _enemyStopMovement;
        public override EnemyKnockback EnemyKnockback => _enemyKnockback;
        public SkeletonDrawChaseOverlay SkeletonDrawChaseOverlay => _skeletonDrawChaseOverlay;
        public bool IsBlocking { get; set; }
        public bool IsEnemyNearToPlayer { get; set; }

        public bool HasLineOfSight => _enemyLineOfSight.HasLineOfSight(collider, PlayerCollider, "Player", PlayerLayerMask);

        [Inject] public IInstantiator Instantiator;

        public SkeletonChaseState SkeletonChaseState
        {
            get;
            private set;
        }
        
        public SkeletonDeathState SkeletonDeathState
        {
            get;
            private set;
        }
        
        public SkeletonHurtState SkeletonHurtState
        {
            get;
            private set;
        }
        
        public SkeletonIdleState SkeletonIdleState
        {
            get;
            private set;
        }
        
        public SkeletonPatrolState SkeletonPatrolState
        {
            get;
            private set;
        }

        public SkeletonAttackBasicState SkeletonAttackBasicState
        {
            get;
            private set;
        }
        
        public SkeletonAttackHeavyState SkeletonAttackHeavyState
        {
            get;
            private set;
        }
        
        public SkeletonBlockState SkeletonBlockState
        {
            get;
            private set;
        }
        

        private void Awake()
        {
            _healthController = new HealthController(100, 100);
            _enemyFacing = new EnemyFacing();
            _enemyFindClosestChasePoint = new EnemyFindClosestChasePoint();
            _enemyLineOfSight = new EnemyLineOfSight();
            _enemySetDestination = new EnemySetDestination();
            _enemyStopMovement = new EnemyStopMovement();
            _enemyKnockback = new EnemyKnockback();
            _skeletonDrawChaseOverlay = new SkeletonDrawChaseOverlay();
            
            SkeletonChaseState = Instantiator.Instantiate<SkeletonChaseState>(new object[]{this});
            SkeletonDeathState = Instantiator.Instantiate<SkeletonDeathState>(new object[]{this});
            SkeletonHurtState = Instantiator.Instantiate<SkeletonHurtState>(new object[]{this});
            SkeletonIdleState = Instantiator.Instantiate<SkeletonIdleState>(new object[]{this});
            SkeletonPatrolState = Instantiator.Instantiate<SkeletonPatrolState>(new object[]{this});
            SkeletonAttackBasicState = Instantiator.Instantiate<SkeletonAttackBasicState>(new object[]{this});
            SkeletonAttackHeavyState = Instantiator.Instantiate<SkeletonAttackHeavyState>(new object[]{this});
            SkeletonBlockState = Instantiator.Instantiate<SkeletonBlockState>(new object[]{this});

            navMeshAgent.speed = EnemyProperties.WalkSpeed;
        }

        private void Start()
        {
            SwitchState(SkeletonIdleState);
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
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, EnemyProperties.ChaseRadius);
        }
#endif
    }
}