using UnityEngine;

namespace Player
{
    public class PlayerWalkState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Walk;
        
        public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            
        }

        public override void OnTick()
        {
            Movement(PlayerStateMachine.InputReader.MovementValue);
        }
        
        public override void OnExit()
        {
            
        }

        private void Movement(Vector2 movement)
        {
            movement = movement.normalized * PlayerStateMachine.WalkSpeed;
            PlayerStateMachine.RigidBody.velocity = movement;
        }
    }
}
