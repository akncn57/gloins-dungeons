using System;
using System.Collections.Generic;
using HealthSystem;
using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonStateMachine : EnemyBaseStateMachine
    {
        [SerializeField] private HealthController healthController;
        [SerializeField] private SkeletonColliderController skeletonColliderController;
        [SerializeField] private SkeletonAnimationEventTrigger skeletonAnimationEventTrigger;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D collider;
        [SerializeField] private CircleCollider2D chaseCollider;
        [SerializeField] private ParticleSystem hurtParticle;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private float walkSpeed;

        public override HealthController HealthController => healthController;
        public override EnemyColliderBaseController EnemyColliderController => skeletonColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => skeletonAnimationEventTrigger;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CircleCollider2D ChaseCollider => chaseCollider;
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => hurtParticle;
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override float WalkSpeed => walkSpeed;

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
        

        private void Awake()
        {
            SkeletonChaseState = new SkeletonChaseState(this);
            SkeletonDeathState = new SkeletonDeathState(this);
            SkeletonHurtState = new SkeletonHurtState(this);
            SkeletonIdleState = new SkeletonIdleState(this);
            SkeletonPatrolState = new SkeletonPatrolState(this);
        }

        private void Start()
        {
            SwitchState(SkeletonIdleState);
        }
    }
}

//TODO: Uygun bir yere yada ayri bir script olarak ac.
[Serializable]
public class EnemyPatrolData
{
    public Transform PatrolCoordinate;
    public bool IsShouldWait;
}

//TODO: Uygun bir yere yada ayri bir script olarak ac.
public class EnemyHitData
{
    public Vector3 HitPosition;
    public int Damage;
    public float KnockBackStrength;

    public EnemyHitData(Vector3 hitPosition, int damage, float knockBackStrength)
    {
        HitPosition = hitPosition;
        Damage = damage;
        KnockBackStrength = knockBackStrength;
    }
}