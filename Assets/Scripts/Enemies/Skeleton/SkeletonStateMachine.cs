using HealthSystem;
using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonStateMachine : EnemyBaseStateMachine
    {
        [SerializeField] private HealthController healthController;
        [SerializeField] private SkeletonColliderController skeletonColliderController;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;

        public override HealthController HealthController => healthController;
        public override EnemyColliderBaseController EnemyColliderController => skeletonColliderController;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Animator Animator => animator;

        private void Start()
        {
            SwitchState(new SkeletonIdleState(this));
        }
    }
}