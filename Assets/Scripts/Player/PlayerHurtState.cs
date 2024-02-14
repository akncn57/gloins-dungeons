using UnityEngine;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Player_Hurt");
        
        public PlayerHurtState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtStart += HurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtEnd += HurtEnd;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtStart -= HurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtEnd -= HurtEnd;
        }

        private void HurtStart()
        {
            PlayerStateMachine.RigidBody.velocity = new Vector2(5f, PlayerStateMachine.RigidBody.velocity.y);
        }
        
        private void HurtEnd()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
    }
}