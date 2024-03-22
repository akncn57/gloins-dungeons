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
            PlayerStateMachine.InputReader.AttackBasicEvent += CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart += CheckOnHurt;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            if (PlayerStateMachine.InputReader.MovementValue == Vector2.zero)
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
            
            if (PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(new PlayerBlockState(PlayerStateMachine));
            
            Movement(PlayerStateMachine.InputReader.MovementValue);
            Facing(PlayerStateMachine.InputReader.MovementValue.x);
        }
        
        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart -= CheckOnHurt;
        }

        private void Movement(Vector2 movement)
        {
            movement = movement.normalized * PlayerStateMachine.WalkSpeed;
            PlayerStateMachine.RigidBody.velocity = movement;
        }

        private void Facing(float horizontalMovement)
        {
            PlayerStateMachine.ParentObject.transform.localScale = horizontalMovement switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => PlayerStateMachine.ParentObject.transform.localScale
            };
        }

        private void CheckAttackBasic()
        {
            PlayerStateMachine.SwitchState(new PlayerAttackBasicState(PlayerStateMachine));
        }
        
        private void CheckOnHurt(Vector3 hitPosition, int damage, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition, damage, knockBackStrength));
        }
    }
}