using System.Collections.Generic;
using CustomInterfaces;
using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MageChaseState : MageBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Mage_Walk");
        private List<Transform> _enemyChasePositionsList;
        
        public MageChaseState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            MageStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            ApproachPlayer(FindClosestPosition(MageStateMachine.Rigidbody.position));
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
        
        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((MageStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 0.1f)
            {
                MageStateMachine.SwitchState(MageStateMachine.MageAttackBasicState);
                return;
            }
            
            var movement = playerPosition - MageStateMachine.Rigidbody.transform.position;
            MageStateMachine.Rigidbody.velocity = movement.normalized * MageStateMachine.WalkSpeed;
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
            MageStateMachine.ParentObject.transform.localScale = horizontalMovement switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => MageStateMachine.ParentObject.transform.localScale
            };
        }
        
        public void Init(IPlayer player)
        {
            _enemyChasePositionsList = player.EnemyChasePositions;
        }
    }
}