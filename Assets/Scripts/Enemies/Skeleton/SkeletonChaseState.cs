using System.Collections.Generic;
using CustomInterfaces;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private List<Transform> _enemyChasePositionsList;

        public SkeletonChaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            ApproachPlayer(FindClosestPosition(SkeletonStateMachine.Rigidbody.position));
        }

        public override void OnExit()
        {
            
        }

        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 0.1f)
            {
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackBasicState);
                return;
            }
            
            var movement = playerPosition - SkeletonStateMachine.Rigidbody.transform.position;
            SkeletonStateMachine.Rigidbody.velocity = movement.normalized * SkeletonStateMachine.WalkSpeed;
            Facing(movement.x);
        }
        
        private Vector3 FindClosestPosition(Vector3 targetPosition)
        {
            var minDistance = Mathf.Infinity;
            var closestPosition = Vector3.zero;
            
            foreach (var enemyChasePosition in _enemyChasePositionsList)
            {
                var distance = Vector3.Distance(targetPosition, enemyChasePosition.position);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPosition = enemyChasePosition.position;
                }
            }

            return closestPosition;
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
            _enemyChasePositionsList = player.EnemyChasePositions;
        }
    }
}