using UnityEngine;

namespace Enemies.UndeadSwordsman.States
{
    public class HeavyAttackState : UndeadSwordsmanBaseState
    {
        public HeavyAttackState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.HeavyAttack);
            
            Context.CameraShake.TriggerShake(2, 0.3f);
            
            Context.LastAttackTime = Time.time;
        }
        
        public override void Update()
        {
        }

        public override void OnHeavyAttackAnimationEndCommand()
        {
            StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
        }
    }
}