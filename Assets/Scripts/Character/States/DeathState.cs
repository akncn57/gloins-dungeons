using StateMachine;

namespace Character.States
{
    public class DeathState : State<CharacterController>
    {
        public DeathState(CharacterController context, BaseStateMachine<CharacterController> baseStateMachine) : base(context, baseStateMachine) {}
    }
}