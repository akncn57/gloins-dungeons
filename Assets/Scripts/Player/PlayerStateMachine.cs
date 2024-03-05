using System;
using HealthSystem;
using InputSystem;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        public InputReader InputReader;
        public PlayerAnimationEventsTrigger PlayerAnimationEventsTrigger;
        public PlayerColliderController PlayerColliderController;
        [NonSerialized] public HealthController HealthController;
        public Rigidbody2D RigidBody;
        public Animator Animator;
        public GameObject ParentObject;
        public GameObject AttackBasicColliderObject;
        public GameObject BlockColliderObject;
        public float WalkSpeed;

        private void Awake()
        {
            HealthController = new HealthController(100, 100, 0);
        }

        private void Start()
        {
            SwitchState(new PlayerIdleState(this));
        }
    }
}
