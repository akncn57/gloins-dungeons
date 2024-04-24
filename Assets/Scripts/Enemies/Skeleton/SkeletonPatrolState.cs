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
            
            if (_patrolIndex >= SkeletonStateMachine.PatrolCoordinates.Count) _patrolIndex = 0;
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

            var movement = coordinate - SkeletonStateMachine.Rigidbody.transform.position;
            SkeletonStateMachine.Rigidbody.velocity = movement.normalized * SkeletonStateMachine.WalkSpeed;
            Facing(movement.x);
        }
        
        private void Facing(float horizontalMovement)
        {
            SkeletonStateMachine.ParentObject.transform.localScale = horizontalMovement switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => SkeletonStateMachine.ParentObject.transform.localScale
            };
        }
    }
}