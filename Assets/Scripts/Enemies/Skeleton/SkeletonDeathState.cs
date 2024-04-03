using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonDeathState : EnemyBaseState
    {
        private readonly int _deathAnimationHash = Animator.StringToHash("Skeleton_Death");
        
        public SkeletonDeathState(EnemyBaseStateMachine enemyStateMachine) : base(enemyStateMachine){}

        public override void OnEnter()
        {
            Debug.Log("Enemy Skeleton Death!");
            EnemyStateMachine.Collider.enabled = false;
            EnemyStateMachine.Rigidbody.velocity = Vector2.zero;
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_deathAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}