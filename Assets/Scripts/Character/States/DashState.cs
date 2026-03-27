using StateMachine;
using UnityEngine;

namespace Character.States
{
    public class DashState : CharacterStateBase
    {
        private float _stateStartTime;
        private Vector2 _dashDirection;
        
        public DashState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}
        
        public override void Enter()
        {
            _stateStartTime = Time.time;
            Context.LastDashTime = Time.time;

            if (Context.MovementInput.magnitude > 0.1f)
            {
                _dashDirection = Context.MovementInput.normalized;
            }
            else
            {
                _dashDirection = Context.SpriteRenderer.flipX ? Vector2.left : Vector2.right;
            }

            if (Context.SpriteRenderer.flipX)
            {
                Context.DashVFXRight.SetActive(true);
            }
            else
            {
                Context.DashVFXLeft.SetActive(true);
            }
        }

        public override void FixedUpdate()
        {
            Context.Rb.linearVelocity = _dashDirection * Context.CharacterStats.DashSpeed;
        }

        public override void Update()
        {
            if (Time.time >= _stateStartTime + Context.CharacterStats.DashDuration)
            {
                if (Context.MovementInput.magnitude > 0.1f)
                {
                    CharacterStateMachine.ChangeState(CharacterStateMachine.WalkState);
                }
                else
                {
                    CharacterStateMachine.ChangeState(CharacterStateMachine.IdleState);
                }
            }
        }
    }
}