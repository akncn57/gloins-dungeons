using System.Collections.Generic;
using Enemies.Skeleton;
using HealthSystem;
using HitData;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Enemies.Mage
{
    public class MageStateMachine : EnemyBaseStateMachine
    {
        [SerializeField] private MageColliderController mageColliderController;
        [SerializeField] private MageAnimationEventTrigger mageAnimationEventTrigger;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D collider;
        [SerializeField] private CapsuleCollider2D attackBasicCollider;
        [SerializeField] private GameObject parentObject;
        [SerializeField] private CircleCollider2D chaseCollider;
        [SerializeField] private ParticleSystem hurtParticle;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float chasePositionOffset;
        
        private HealthController _healthController;
        
        public override HealthController HealthController => _healthController;
        public override EnemyColliderBaseController EnemyColliderController => mageColliderController;
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger => mageAnimationEventTrigger;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CapsuleCollider2D AttackBasicCollider => attackBasicCollider;
        public override GameObject ParentObject => parentObject;
        public override CircleCollider2D ChaseCollider => chaseCollider;
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle => hurtParticle;
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override float WalkSpeed => walkSpeed;
        public override float ChasePositionOffset => chasePositionOffset;
        
        [Inject] public IInstantiator Instantiator;

        public MageChaseState MageChaseState
        {
            get;
            private set;
        }
        
        public MageDeathState MageDeathState
        {
            get;
            private set;
        }
        
        public MageHurtState MageHurtState
        {
            get;
            private set;
        }
        
        public MageIdleState MageIdleState
        {
            get;
            private set;
        }
        
        public MagePatrolState MagePatrolState
        {
            get;
            private set;
        }

        public MageAttackBasicState MageAttackBasicState
        {
            get;
            private set;
        }
        
        public MageAttackHeavyState MageAttackHeavyState
        {
            get;
            private set;
        }
        

        private void Awake()
        {
            _healthController = new HealthController(100, 100);
            
            MageChaseState = Instantiator.Instantiate<MageChaseState>(new object[]{this});
            MageDeathState = Instantiator.Instantiate<MageDeathState>(new object[]{this});
            MageHurtState = Instantiator.Instantiate<MageHurtState>(new object[]{this});
            MageIdleState = Instantiator.Instantiate<MageIdleState>(new object[]{this});
            MagePatrolState = Instantiator.Instantiate<MagePatrolState>(new object[]{this});
            MageAttackBasicState = Instantiator.Instantiate<MageAttackBasicState>(new object[]{this});
            MageAttackHeavyState = Instantiator.Instantiate<MageAttackHeavyState>(new object[]{this});
        }

        private void Start()
        {
            SwitchState(MageIdleState);
        }

        public void ResetPatrolCoordinateStatus()
        {
            foreach (var coordinate in PatrolCoordinates)
            {
                coordinate.IsCompleted = false;
            }
        }
    }
}