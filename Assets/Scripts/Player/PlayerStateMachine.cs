using System;
using System.Collections.Generic;
using CustomInterfaces;
using HealthSystem;
using InputSystem;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine, IPlayer
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
        public List<Transform> EnemyChasePositionsList;
        public PlayerHitData HitData;
        
        [Inject] public IInstantiator Instantiator;

        public List<Transform> EnemyChasePositions
        {
            get => EnemyChasePositionsList;
            set => EnemyChasePositionsList = value;
        }
        
        public PlayerAttackBasicState PlayerAttackBasicState
        {
            get;
            private set;
        }
        
        public PlayerBlockState PlayerBlockState
        {
            get;
            private set;
        }
        
        public PlayerDeathState PlayerDeathState
        {
            get;
            private set;
        }
        
        public PlayerHurtState PlayerHurtState
        {
            get;
            private set;
        }
        
        public PlayerIdleState PlayerIdleState
        {
            get;
            private set;
        }
        
        public PlayerWalkState PlayerWalkState
        {
            get;
            private set;
        }

        private void Start()
        {
            PlayerAttackBasicState = Instantiator.Instantiate<PlayerAttackBasicState>(new object[]{this});
            PlayerBlockState = Instantiator.Instantiate<PlayerBlockState>(new object[]{this});
            PlayerDeathState = Instantiator.Instantiate<PlayerDeathState>(new object[]{this});
            PlayerHurtState = Instantiator.Instantiate<PlayerHurtState>(new object[]{this});
            PlayerIdleState = Instantiator.Instantiate<PlayerIdleState>(new object[]{this});
            PlayerWalkState = Instantiator.Instantiate<PlayerWalkState>(new object[]{this});
            
            SwitchState(PlayerIdleState);
        }
    }
}

public class PlayerHitData
{
    public Vector3 HitPosition;
    public int Damage;
    public float KnockBackStrength;

    public PlayerHitData(Vector3 hitPosition, int damage, float knockBackStrength)
    {
        HitPosition = hitPosition;
        Damage = damage;
        KnockBackStrength = knockBackStrength;
    }
}
