using UnityEngine;

namespace Character.States
{
    public class DeathState : CharacterStateBase
    {
        public DeathState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}
    }
}