using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class DeathState : UndeadMageBaseState
    {
        public DeathState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadMageAnimatorHashes.Death);
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Rb.simulated = false; // Disable physics interaction
            if(Context.TryGetComponent<Collider2D>(out var col))
            {
                col.enabled = false;
            }
        }
    }
}