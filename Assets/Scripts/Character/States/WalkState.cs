using StateMachine;

namespace Character.States
{
    public class WalkState : State<CharacterController>
    {
        public WalkState(CharacterController context, BaseStateMachine<CharacterController> baseStateMachine) : base(context, baseStateMachine) {}
    }
}