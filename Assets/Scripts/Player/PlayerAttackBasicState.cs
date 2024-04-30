using System.Collections.Generic;
using ColliderController;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerAttackBasicState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.AttackBasic;

        private readonly List<ColliderControllerBase> _hittingEnemies = new();
        
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Player_Attack_Basic");
        
        public PlayerAttackBasicState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapOpen += PlayerOnAttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapClose += PlayerOnAttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicFinished += PlayerOnAttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapOpen -= PlayerOnAttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapClose -= PlayerOnAttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicFinished -= PlayerOnAttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart -= CheckOnHurt;
        }

        private void PlayerOnAttackBasicOpenOverlap()
        {
            var results = Physics2D.OverlapCapsuleAll(PlayerStateMachine.AttackBasicCollider.transform.position, PlayerStateMachine.AttackBasicCollider.size,
                PlayerStateMachine.AttackBasicCollider.direction, 0f);
            
            _hittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (!result) continue;
                var enemy = result.GetComponent<ColliderControllerBase>();
                _hittingEnemies.Add(enemy);
                //TODO: Enemy | Damage, knockbackpower farkli sekilde al.
                enemy.InvokeOnHitStartEvent(10, (enemy.transform.position - PlayerStateMachine.transform.position).normalized, 2f);
            }
        }
        
        private void PlayerOnAttackBasicCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies)
            {
                if (!enemy) continue;
                enemy.InvokeOnHitEndEvent();
            }
            _hittingEnemies.Clear();
        }

        private void PlayerOnAttackBasicFinish()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
        
        private void CheckOnHurt(Vector3 hitPosition, int damage, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition, damage, knockBackStrength));
        }
    }
}