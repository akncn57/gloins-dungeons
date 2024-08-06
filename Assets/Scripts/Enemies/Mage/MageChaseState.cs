using System.Collections.Generic;
using CustomInterfaces;
using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MageChaseState : MageBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Mage_Walk");
        private GameObject _playerGameObject;
        
        public MageChaseState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            MageStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
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
            if ((MageStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 0.1f)
            {
                MageStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < MageStateMachine.Rigidbody.position.x 
                    ? new Vector3(-1f, 1f, 1f) 
                    : new Vector3(1f, 1f, 1f);
                
                MageStateMachine.SwitchState(MageStateMachine.MageAttackBasicState);
                return;
            }
            
            var movement = playerPosition - MageStateMachine.Rigidbody.transform.position;
            MageStateMachine.Rigidbody.velocity = movement.normalized * MageStateMachine.WalkSpeed;
            Facing(movement.x);
        }

        private Vector3 FindClosestPosition()
        {
            var playerPosition = _playerGameObject.transform.position;
            return MageStateMachine.Rigidbody.position.x <= playerPosition.x 
                ? new Vector3(playerPosition.x - MageStateMachine.ChasePositionOffset, playerPosition.y, 0f) 
                : new Vector3(playerPosition.x + MageStateMachine.ChasePositionOffset, playerPosition.y, 0f);
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
            _playerGameObject = player.GameObject;
        }
    }
}