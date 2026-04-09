namespace Enemies.UndeadSwordsman.States
{
    public class LightAttackState : UndeadSwordsmanBaseState
    {
        public LightAttackState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}
        
        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.LightAttack);
            Context.Rb.linearVelocity = UnityEngine.Vector2.zero;
            
            // Record the time this attack started for cooldown tracking
            Context.LastAttackTime = UnityEngine.Time.time;
        }

        public override void Update()
        {
            // Cooldown is now tracked in ChaseState. We stay in this state until the animation event calls OnLightAttackAnimationEndCommand.
        }

        public override void OnLightAttackAnimationEndCommand()
        {
            StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
        }
    }
}