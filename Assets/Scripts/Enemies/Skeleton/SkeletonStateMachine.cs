using System;
using System.Collections.Generic;
using HealthSystem;
using HitData;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonStateMachine : EnemyBaseStateMachine
    {
        [SerializeField] private SkeletonColliderController skeletonColliderController;
        [SerializeField] private SkeletonAnimationEventTrigger skeletonAnimationEventTrigger;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D collider;
        [SerializeField] private CapsuleCollider2D attackBasicCollider;
        [SerializeField] private GameObject parentObject;
        [SerializeField] private CircleCollider2D chaseCollider;
        [SerializeField] private ParticleSystem hurtParticle;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float chasePositionOffset;
        
        private HealthController _healthController;
        private EnemyMover _enemyMover;
        private EnemyFacing _enemyFacing;

        public override HealthController HealthController => _healthController;
        public override EnemyColliderBaseController EnemyColliderController => skeletonColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => skeletonAnimationEventTrigger;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CapsuleCollider2D AttackBasicCollider => attackBasicCollider;
        public override GameObject ParentObject => parentObject;
        public override CircleCollider2D ChaseCollider => chaseCollider;
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => hurtParticle;
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override float WalkSpeed => walkSpeed;
        public override float ChasePositionOffset => chasePositionOffset;
        public override EnemyMover EnemyMover => _enemyMover;
        public override EnemyFacing EnemyFacing => _enemyFacing;

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
        

        private void Awake()
        {
            _healthController = new HealthController(100, 100);
            _enemyMover = new EnemyMover();
            _enemyFacing = new EnemyFacing();
            
            SkeletonChaseState = Instantiator.Instantiate<SkeletonChaseState>(new object[]{this});
            SkeletonDeathState = Instantiator.Instantiate<SkeletonDeathState>(new object[]{this});
            SkeletonHurtState = Instantiator.Instantiate<SkeletonHurtState>(new object[]{this});
            SkeletonIdleState = Instantiator.Instantiate<SkeletonIdleState>(new object[]{this});
            SkeletonPatrolState = Instantiator.Instantiate<SkeletonPatrolState>(new object[]{this});
            SkeletonAttackBasicState = Instantiator.Instantiate<SkeletonAttackBasicState>(new object[]{this});
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
    }
}