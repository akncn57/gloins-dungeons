using CustomInterfaces;
using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MagePatrolState : MageBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Mage_Walk");
        private int _patrolIndex;
        
        public MagePatrolState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}

        public override void OnEnter()
        {
            MageStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            MageStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);

            if (_patrolIndex >= MageStateMachine.PatrolCoordinates.Count)
            {
                MageStateMachine.ResetPatrolCoordinateStatus();
                MageStateMachine.PatrolCoordinates.Reverse();
                _patrolIndex = 1;
            }
        }

        public override void OnTick()
        {
            DrawChaseOverlayAndCheck();
            
            if (!MageStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted)
                GoPatrolCoordinate(MageStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
        }

        public override void OnExit()
        {
            MageStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            
            _patrolIndex++;
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            MageStateMachine.SwitchState(MageStateMachine.MageHurtState);
        }
        
        private void DrawChaseOverlayAndCheck()
        {
            var results = Physics2D.OverlapCircleAll(MageStateMachine.ChaseCollider.transform.position, MageStateMachine.ChaseCollider.radius);
            
            foreach (var result in results)
            {
                if (!result) continue;

                if (!result.TryGetComponent(out IPlayer player))
                    player = result.GetComponentInParent<IPlayer>();

                if (player != null)
                {
                    MageStateMachine.MageChaseState.Init(player);
                    MageStateMachine.SwitchState(MageStateMachine.MageChaseState);
                }
            }
        }
        
        private void GoPatrolCoordinate(Vector3 coordinate)
        {
            if ((MageStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.1f)
            {
                MageStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true;
                MageStateMachine.Rigidbody.velocity = Vector2.zero;
                MageStateMachine.SwitchState(MageStateMachine.MageIdleState);
                return;
            }

            var movement = coordinate - MageStateMachine.Rigidbody.transform.position;
            MageStateMachine.Rigidbody.velocity = movement.normalized * MageStateMachine.EnemyProperties.WalkSpeed;
            Facing(movement.x);
        }
        
        private void Facing(float horizontalMovement)
        {
            MageStateMachine.ParentObject.transform.localScale = horizontalMovement switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => MageStateMachine.ParentObject.transform.localScale
            };
        }
    }
}