using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerAttackBasicState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.AttackBasic;

        private readonly List<EnemyColliderBaseController> _hittingEnemies = new();
        
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Player_Attack_Basic");
        
        public PlayerAttackBasicState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderOpen += AttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderClose += AttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicFinished += AttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderOpen -= AttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderClose -= AttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicFinished -= AttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.PlayerOnHitStart -= CheckOnHurt;
        }

        private void AttackBasicOpenOverlap()
        {
            var results = Physics2D.OverlapCapsuleAll(PlayerStateMachine.AttackBasicCollider.transform.position, PlayerStateMachine.AttackBasicCollider.size,
                PlayerStateMachine.AttackBasicCollider.direction, 0f);
            
            _hittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (!result) continue;
                var enemy = result.GetComponent<EnemyColliderBaseController>();
                _hittingEnemies.Add(enemy);
                enemy.InvokeOnHitStartEvent(10);
            }
        }
        
        private void AttackBasicCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies)
            {
                if (!enemy) continue;
                enemy.InvokeOnHitEndEvent();
            }
            _hittingEnemies.Clear();
        }

        private void AttackBasicFinish()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
        
        private void CheckOnHurt(Vector3 hitPosition, int damage, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition, damage, knockBackStrength));
        }
    }
}