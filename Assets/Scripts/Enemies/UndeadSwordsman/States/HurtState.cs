namespace Enemies.UndeadSwordsman.States
{
    public class HurtState : UndeadSwordsmanBaseState
    {
        public HurtState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.Hurt);
        }
    }
}