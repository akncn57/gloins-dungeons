namespace Enemies.UndeadSwordsman.States
{
    public class DeathState : UndeadSwordsmanBaseState
    {
        public DeathState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.Death);
            Context.Rb.linearVelocity = UnityEngine.Vector2.zero; // Stop moving
            
            // Destroy after 1.5 seconds (gives time for death animation and fade out effects if any)
            UnityEngine.Object.Destroy(Context.gameObject, 1.5f);
        }
    }
}