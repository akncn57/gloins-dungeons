using UnityEngine;

namespace Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Idle;
        
        private readonly int _idleAnimationHash = Animator.StringToHash("Player_Idle");
        
        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent += CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            if (PlayerStateMachine.InputReader.MovementValue != Vector2.zero)
                PlayerStateMachine.SwitchState(new PlayerWalkState(PlayerStateMachine));
            
            if (PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(new PlayerBlockState(PlayerStateMachine));
        }

        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
        }
        
        private void CheckAttackBasic()
        {
            PlayerStateMachine.SwitchState(new PlayerAttackBasicState(PlayerStateMachine));
        }

        private void CheckOnHurt(Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition, knockBackStrength));
        }
    }
}