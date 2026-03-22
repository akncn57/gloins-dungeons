using StateMachine;

namespace Character.States
{
    public class HurtState : State<CharacterController>
    {
        public HurtState(CharacterController context, BaseStateMachine<CharacterController> stateMachine) : base(context, stateMachine) {}
    }
}