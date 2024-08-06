using System.Collections.Generic;
using CustomInterfaces;
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
        }

        public override void OnTick()
        {
            ApproachPlayer(FindClosestPosition());
        }

        public override void OnExit()
        {
            
        }

        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 0.1f)
            {
                SkeletonStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < SkeletonStateMachine.Rigidbody.position.x 
                    ? new Vector3(-1f, 1f, 1f) 
                    : new Vector3(1f, 1f, 1f);
                
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackBasicState);
                return;
            }
            
            var movement = playerPosition - SkeletonStateMachine.Rigidbody.transform.position;
            SkeletonStateMachine.Rigidbody.velocity = movement.normalized * SkeletonStateMachine.WalkSpeed;
            Facing(movement.x);
        }
        
        private Vector3 FindClosestPosition()
        {
            var playerPosition = _playerGameObject.transform.position;
            return SkeletonStateMachine.Rigidbody.position.x <= playerPosition.x 
                ? new Vector3(playerPosition.x - SkeletonStateMachine.ChasePositionOffset, playerPosition.y, 0f) 
                : new Vector3(playerPosition.x + SkeletonStateMachine.ChasePositionOffset, playerPosition.y, 0f);
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

        public void Init(IPlayer player)
        {
            _playerGameObject = player.GameObject;
        }
    }
}