using System;
using InputSystem;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        public InputReader InputReader;
        public Rigidbody2D RigidBody;
        public Animator Animator;
        public float WalkSpeed;

        private void Start()
        {
            SwitchState(new PlayerIdleState(this));
        }
    }
}
