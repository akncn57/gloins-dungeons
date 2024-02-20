using UnityEngine;

namespace Player
{
    public class PlayerBlockState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Block;
        
        private readonly int _blockUpAnimationHash = Animator.StringToHash("Player_Block_Up");
        private readonly int _blockDownAnimationHash = Animator.StringToHash("Player_Block_Down");
        private readonly int _blockIdleAnimationHash = Animator.StringToHash("Player_Block_Idle");
        
        public PlayerBlockState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            
            PlayerStateMachine.BlockColliderObject.SetActive(true);
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_blockUpAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            
            if (!PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
            
            //TODO: Block idle animation.
            // PlayerStateMachine.Animator.CrossFadeInFixedTime(_blockIdleAnimationHash, 0.1f);
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.BlockColliderObject.SetActive(false);
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_blockDownAnimationHash, 0.1f);
        }
        
        private void CheckOnHurt(Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition, knockBackStrength));
        }
    }
}