using UnityEngine;

namespace Enemies.UndeadSwordsman.States
{
    public class HeavyAttackState : UndeadSwordsmanBaseState
    {
        private float _attackTimer;
        
        public HeavyAttackState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.HeavyAttack);
            
            Context.CameraShake.TriggerShake(2, 0.3f);
            
            _attackTimer = ((UndeadSwordsmanStatsSO)Context.EnemyStats).LightAttackAttackCooldown;
        }
        
        public override void Update()
        {
            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0)
            {
                StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
            }
        }
    }
}