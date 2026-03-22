using StateMachine;

namespace Character.States
{
    public class LightAttackState : State<CharacterController>
    {
        public LightAttackState(CharacterController context, BaseStateMachine<CharacterController> stateMachine) : base(context, stateMachine) {}
    }
}