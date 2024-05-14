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
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            CheckStopMoving();
            CheckBlocking();
        }

        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }
        
        private void CheckStopMoving()
        {
            if (PlayerStateMachine.InputReader.MovementValue == Vector2.zero)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }

        private void CheckBlocking()
        {
            if (PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerBlockState);
        }
        
        private void CheckAttackBasic()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerAttackBasicState);
        }

        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}