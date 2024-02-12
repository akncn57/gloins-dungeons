using UnityEngine;

namespace Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Idle;
        
        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            
        }

        public override void OnTick()
        {
            if (PlayerStateMachine.InputReader.MovementValue != Vector2.zero)
                PlayerStateMachine.SwitchState(new PlayerWalkState(PlayerStateMachine));
        }

        public override void OnExit()
        {
            
        }
    }
}
