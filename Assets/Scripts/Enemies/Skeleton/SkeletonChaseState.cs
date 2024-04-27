using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private GameObject _playerGameObject;

        public SkeletonChaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
            
            Debug.Log("Enemy Chase State");
        }

        public override void OnTick()
        {
            ApproachPlayer(_playerGameObject.transform.position);
        }

        public override void OnExit()
        {
            
        }

        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 1f)
            {
                SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
                Debug.Log("Skeleton Attack Basic State!");
                return;
            }
            
            var movement = playerPosition - SkeletonStateMachine.Rigidbody.transform.position;
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

        public void Init(GameObject playerGameObject)
        {
            _playerGameObject = playerGameObject;
        }
    }
}