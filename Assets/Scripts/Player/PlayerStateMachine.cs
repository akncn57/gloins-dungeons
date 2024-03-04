using System;
using InputSystem;
using LifeSystem;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        public InputReader InputReader;
        public PlayerAnimationEventsTrigger PlayerAnimationEventsTrigger;
        public PlayerColliderController PlayerColliderController;
        [NonSerialized] public LifeController LifeController;
        public Rigidbody2D RigidBody;
        public Animator Animator;
        public GameObject ParentObject;
        public GameObject AttackBasicColliderObject;
        public GameObject BlockColliderObject;
        public float WalkSpeed;

        private void Awake()
        {
            LifeController = new LifeController(100, 100, 0);
        }

        private void Start()
        {
            SwitchState(new PlayerIdleState(this));
        }
    }
}
