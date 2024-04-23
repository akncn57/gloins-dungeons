using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonPatrolState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private int _patrolIndex;
        
        public SkeletonPatrolState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
            
            if (_patrolIndex > SkeletonStateMachine.PatrolCoordinates.Count) _patrolIndex = 0;
        }

        public override void OnTick()
        {
            GoPatrolCoordinate(SkeletonStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
        }

        public override void OnExit()
        {
            _patrolIndex++;
        }

        private void GoPatrolCoordinate(Vector3 coordinate)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.1f)
            {
                SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                return;
            }
            
            SkeletonStateMachine.Rigidbody.velocity = (coordinate - SkeletonStateMachine.Rigidbody.transform.position).normalized * SkeletonStateMachine.WalkSpeed;
        }
    }
}