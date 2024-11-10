using System.Collections.Generic;
using HealthSystem;
using HitData;
using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class EnemyBaseStateMachine : BaseStateMachine
    {
        public abstract EnemyProperties EnemyProperties { get; }
        public abstract HealthController HealthController { get; }
        public abstract EnemyColliderBaseController EnemyColliderController { get; }
        public abstract EnemyAnimationEventTrigger EnemyAnimationEventTrigger { get; }
        public abstract NavMeshAgent EnemyNavMeshAgent { get; }
        public abstract Rigidbody2D Rigidbody { get; }
        public abstract Collider2D Collider { get; }
        public abstract CapsuleCollider2D AttackBasicCollider { get; }
        public abstract GameObject ParentObject { get; }
        public abstract GameObject ExclamationMarkObject { get; }
        public abstract Animator Animator { get; }
        public abstract ParticleSystem HurtParticle { get; }
        public abstract ParticleSystem BlockEffectParticle { get; }
        public abstract List<EnemyPatrolData> PatrolCoordinates { get; }
        public abstract EnemyHitData HitData { get; set; }
        public abstract EnemyFacing EnemyFacing { get; }
        public abstract EnemyFindClosestChasePoint EnemyFindClosestChasePoint { get; }
        public abstract EnemyLineOfSight EnemyLineOfSight { get; }
        public abstract EnemySetDestination EnemySetDestination { get; }
        public abstract EnemyStopMovement EnemyStopMovement { get; }
    }
}