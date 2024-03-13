using UnityEngine;

namespace Player
{
    public class PlayerDeathState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Death;
        
        private readonly int _deathAnimationHash = Animator.StringToHash("Player_Death");
        
        public PlayerDeathState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            Debug.Log("Player Death!");
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_deathAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}