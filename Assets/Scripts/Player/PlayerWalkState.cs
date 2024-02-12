using UnityEngine;

namespace Player
{
    public class PlayerWalkState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Walk;
        
        private readonly int _walkAnimationHash = Animator.StringToHash("Player_Walk");
        
        public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            if (PlayerStateMachine.InputReader.MovementValue == Vector2.zero)
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
            
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
