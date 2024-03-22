using Enemies.Skeleton;
using HealthSystem;
using StateMachine;
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyBaseStateMachine : BaseStateMachine
    {
        public abstract HealthController HealthController { get; }
        public abstract EnemyColliderBaseController EnemyColliderController { get; }
        public abstract Rigidbody2D Rigidbody { get; }
        public abstract Animator Animator { get; }
    }
}