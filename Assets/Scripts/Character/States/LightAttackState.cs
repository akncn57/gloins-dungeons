using UnityEngine;

namespace Character.States
{
    public class LightAttackState : CharacterStateBase
    {
        public LightAttackState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            // Soft Lock-on & Lunge
            Transform target = Context.CombatController.CurrentTarget;
            // Fallback (eğer Update döngüsü henüz yakalayamadıysa anlık hesapla)
            if (target == null) target = Context.CombatController.GetBestTarget(Context.MovementInput);
            
            if (target != null)
            {
                // Yüzünü hedefe dön
                Context.SpriteRenderer.flipX = target.position.x < Context.transform.position.x;
                
                // Lunge (Atılma - Hissi kuvvetlendirir)
                Vector2 lungeDirection = (target.position - Context.transform.position).normalized;
                Context.Rb.linearVelocity = lungeDirection * 4f; 
            }
            else
            {
                Context.Rb.linearVelocity = Vector2.zero;
            }

            Context.Animator.SetTrigger(CharacterAnimatorHashes.LightAttack);
            Context.CameraShake.TriggerShake(0.5f, 0.3f);
            
            // GAME FEEL: Hafif vuruş, çok seri ve anlık bir eylemdir. Bu yüzden sadece çok küçük
            // bir esneme (1.1x yatay, 0.9x dikey) yapıyoruz ve süresini çok kısa tutuyoruz (0.1s).
            Context.SquashAndStretch(new Vector2(1.2f, 0.9f), 0.1f);
        }

        public override void OnLightAttackAnimationEndCommand()
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