using System;

namespace Character
{
    public class CharacterStateMachine : StateMachine
    {
        public CharacterIdleState IdleState { get; private set; }
        public CharacterWalkState WalkState { get; private set; }
        
        private void Awake()
        {
            IdleState = new CharacterIdleState(this);
            WalkState = new CharacterWalkState(this);
        }
    }
}