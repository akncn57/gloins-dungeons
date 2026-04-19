using UnityEngine;

namespace Character.States
{
    public class HurtState : CharacterStateBase
    {
        public HurtState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            // Context.Rb.linearVelocity = Vector2.zero; (Knockback effect in CharacterController should not be overwritten)
            Context.Animator.SetTrigger(CharacterAnimatorHashes.Hurt);
            Context.BloodVFX.SetActive(true);
            
            // GAME FEEL: Oyuncu hasar aldığında dikeyde ezilmiş hissiyatı vermek için (üstten yumruk yemiş gibi)
            // dikeyde daraltıp yatayda genişletiyoruz (1.3x, 0.7x)
            Context.SquashAndStretch(new Vector2(1.3f, 0.7f), 0.2f);
        }

        public override void OnHurtAnimationEndCommand()
        {
            if (Context.Rb.linearVelocity.magnitude > 0.1f)
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