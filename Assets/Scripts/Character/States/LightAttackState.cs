using StateMachine;

namespace Character.States
{
    public class LightAttackState : State<CharacterController>
    {
        public LightAttackState(CharacterController context, BaseStateMachine<CharacterController> baseStateMachine) : base(context, baseStateMachine) {}
    }
}