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
        [SerializeField] private List<PatrolData> patrolCoordinates;
        [SerializeField] private float walkSpeed;

        public override HealthController HealthController => healthController;
        public override EnemyColliderBaseController EnemyColliderController => skeletonColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => skeletonAnimationEventTrigger;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CircleCollider2D ChaseCollider => chaseCollider;
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => hurtParticle;
        public override List<PatrolData> PatrolCoordinates => patrolCoordinates;
        public override float WalkSpeed => walkSpeed;

        private void Start()
        {
            SwitchState(new SkeletonIdleState(this));
        }
    }
}

//TODO: Uygun bir yere yada ayri bir script olarak ac.
[Serializable]
public class PatrolData
{
    public Transform PatrolCoordinate;
    public bool IsShouldWait;
}