using System.Collections.Generic;
using CustomInterfaces;
using HealthSystem;
using HitData;
using InputSystem;
using Player.States;
using StateMachine;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine, IPlayer
    {
        public InputReader InputReader;
        public PlayerProperties PlayerProperties;
        public PlayerAnimationEventsTrigger PlayerAnimationEventsTrigger;
        public PlayerColliderController PlayerColliderController;
        public HealthController HealthController;
        public Rigidbody2D RigidBody;
        public Animator Animator;
        public TrailRenderer TrailRenderer;
        public GameObject ParentObject;
        public CapsuleCollider2D AttackBasicCollider;
        public GameObject BlockColliderObject;
        public ParticleSystem HurtParticle;
        public List<Transform> EnemyChasePositionsList;
        public PlayerHitData HitData;
        public PlayerMover PlayerMover;
        public PlayerFacing PlayerFacing;
        public PlayerAttackBasic PlayerAttackBasic;
        
        [Inject] public IInstantiator Instantiator;

        #region Player State Classes
        
        public List<Transform> EnemyChasePositions
        {
            get => EnemyChasePositionsList;
            set => EnemyChasePositionsList = value;
        }
        
        public GameObject GameObject => gameObject;

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
        
        public PlayerDashState PlayerDashState
        {
            get;
            private set;
        }
        
        #endregion

        private void Start()
        {
            PlayerMover = new PlayerMover();
            PlayerFacing = new PlayerFacing();
            PlayerAttackBasic = new PlayerAttackBasic();
            HealthController = new HealthController(100, 100);
            
            PlayerAttackBasicState = Instantiator.Instantiate<PlayerAttackBasicState>(new object[]{this});
            PlayerBlockState = Instantiator.Instantiate<PlayerBlockState>(new object[]{this});
            PlayerDeathState = Instantiator.Instantiate<PlayerDeathState>(new object[]{this});
            PlayerHurtState = Instantiator.Instantiate<PlayerHurtState>(new object[]{this});
            PlayerIdleState = Instantiator.Instantiate<PlayerIdleState>(new object[]{this});
            PlayerWalkState = Instantiator.Instantiate<PlayerWalkState>(new object[]{this});
            PlayerDashState = Instantiator.Instantiate<PlayerDashState>(new object[]{this});
            
            SwitchState(PlayerIdleState);
        }
    }
}
