using UnityEngine;

namespace Character.States
{
    public class HeavyAttackState : CharacterStateBase
    {
        public HeavyAttackState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.LastHeavyAttackTime = Time.time;
            
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(CharacterAnimatorHashes.HeavyAttack);

            Context.CameraShake.TriggerShake(1, 0.3f);
            
            // GAME FEEL: Ağır vuruşta kılıç çok ağır olduğu için karakter önce yere doğru basıklaşır (squash)
            // Kılıcı savururken ileri doğru süner. Biz burada genel bir vuruş hissiyatı için 
            // dikeyde yassılaştırıp (1.2x uzatıp), yatayda daraltıyoruz (0.8x)
            Context.SquashAndStretch(new Vector2(0.8f, 1.2f), 0.25f);
        }

        public override void OnHeavyAttackAnimationEndCommand()
        {
            if (Context.MovementInput.magnitude > 0.1)
            {
                StateMachine.ChangeState(CharacterStateMachine.WalkState);
            }
            else
            {
                StateMachine.ChangeState(CharacterStateMachine.IdleState);
            }
        }
    }
}