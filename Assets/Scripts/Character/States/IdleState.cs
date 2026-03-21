using StateMachine;

namespace Character.States
{
    public class IdleState : State<CharacterController>
    {
        public IdleState(CharacterController context, BaseStateMachine<CharacterController> baseStateMachine) : base(context, baseStateMachine) {}
    }
}