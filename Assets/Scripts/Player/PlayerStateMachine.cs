using System;
using HealthSystem;
using InputSystem;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        public InputReader InputReader;
        public PlayerAnimationEventsTrigger PlayerAnimationEventsTrigger;
        public PlayerColliderController PlayerColliderController;
        public HealthController HealthController;
        public Rigidbody2D RigidBody;
        public Animator Animator;
        public GameObject ParentObject;
        public CapsuleCollider2D AttackBasicCollider;
        public GameObject BlockColliderObject;
        public ParticleSystem HurtParticle;
        public float WalkSpeed;

        private void Start()
        {
            SwitchState(new PlayerIdleState(this));
        }
    }
}
