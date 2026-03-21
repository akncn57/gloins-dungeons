using StateMachine;

namespace Character.States
{
    public class DashState : State<CharacterController>
    {
        public DashState(CharacterController context, BaseStateMachine<CharacterController> baseStateMachine) : base(context, baseStateMachine) {}
    }
}