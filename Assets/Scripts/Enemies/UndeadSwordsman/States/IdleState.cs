using UnityEngine;

namespace Enemies.UndeadSwordsman.States
{
    public class IdleState : UndeadSwordsmanBaseState
    {
        public IdleState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
        }
    }
}