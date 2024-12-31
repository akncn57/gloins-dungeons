using System.Collections.Generic;
using Enemies.Orc.States;
using HealthSystem;
using HitData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Orc
{
    public class OrcStateMachine : EnemyBaseStateMachine
    {
        [Inject] public IInstantiator Instantiator;
        
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private List<EnemyPatrolData> patrolCoordinates;
        
        public override EnemyProperties EnemyProperties { get; }
        public override HealthController HealthController { get; }
        public override EnemyColliderBaseController EnemyColliderController { get; }
        public override EnemyAnimationEventTrigger EnemyAnimationEventTrigger { get; }
        public override NavMeshAgent EnemyNavMeshAgent => navMeshAgent;
        public override Rigidbody2D Rigidbody => rigidBody;
        public override Collider2D Collider => collider;
        public override CapsuleCollider2D AttackBasicCollider { get; }
        public override BoxCollider2D AttackHeavyCollider { get; }
        public override GameObject ParentObject { get; }
        public override GameObject ExclamationMarkObject { get; }
        public override GameObject WarningObject { get; }
        public override Animator Animator => animator;
        public override ParticleSystem HurtParticle { get; }
        public override ParticleSystem BlockEffectParticle { get; }
        public override List<EnemyPatrolData> PatrolCoordinates => patrolCoordinates;
        public override EnemyHitData HitData { get; set; }
        public override EnemyFacing EnemyFacing { get; }
        public override EnemyLineOfSight EnemyLineOfSight { get; }
        public override EnemySetDestination EnemySetDestination => _enemySetDestination;
        public override EnemyStopMovement EnemyStopMovement => _enemyStopMovement;
        public override EnemyStopRigidbody EnemyStopRigidbody { get; }
        public override EnemyKnockback EnemyKnockback { get; }

        public OrcIdleState OrcIdleState { get; private set; }
        public OrcPatrolState OrcPatrolState { get; private set; }
        
        public void ResetPatrolCoordinateStatus()
        {
            foreach (var coordinate in PatrolCoordinates)
            {
                coordinate.IsCompleted = false;
            }
        }
        
        private EnemySetDestination _enemySetDestination;
        private EnemyStopMovement _enemyStopMovement;

        private void Awake()
        {
            OrcIdleState = Instantiator.Instantiate<OrcIdleState>(new object[]{this});
            OrcPatrolState = Instantiator.Instantiate<OrcPatrolState>(new object[]{this});
            
            _enemySetDestination = new EnemySetDestination();
            _enemyStopMovement = new EnemyStopMovement();
            
            navMeshAgent.speed = 3f;
        }
        
        private void Start()
        {
            SwitchState(OrcIdleState);
        }
    }
}