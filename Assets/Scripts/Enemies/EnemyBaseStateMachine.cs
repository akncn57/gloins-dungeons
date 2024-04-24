using System.Collections.Generic;
using HealthSystem;
using StateMachine;
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyBaseStateMachine : BaseStateMachine
    {
        public abstract HealthController HealthController { get; }
        public abstract EnemyColliderBaseController EnemyColliderController { get; }
        public abstract EnemyAnimationEventTrigger EnemyAnimationEventTrigger { get; }
        public abstract Rigidbody2D Rigidbody { get; }
        public abstract Collider2D Collider { get; }
        public abstract GameObject ParentObject { get; }
        public abstract CircleCollider2D ChaseCollider { get; }
        public abstract Animator Animator { get; }
        public abstract ParticleSystem HurtParticle { get; }
        public abstract List<EnemyPatrolData> PatrolCoordinates { get; }
        public abstract EnemyHitData HitData { get; set; }
        public abstract float WalkSpeed { get; }
    }
}