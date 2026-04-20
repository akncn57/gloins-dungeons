using UnityEngine;

namespace Enemies.UndeadMage
{
    public class UndeadMageAnimationEvents : EnemyAnimationEvents
    {
        private UndeadMageController _undeadMageController;
        
        protected override void Awake()
        {
            base.Awake();
            _undeadMageController = GetComponentInParent<UndeadMageController>();
        }

        public void EndLightAttackInit()
        {
            _undeadMageController?.OnLightAttackInitAnimationEnd();
        }

        public void EndLightAttackFinal()
        {
            _undeadMageController?.OnLightAttackFinalAnimationEnd();
        }
        
        public void EndHeavyAttack()
        {
            _undeadMageController?.OnHeavyAttackAnimationEnd();
        }

        public void EndHurt()
        {
            _undeadMageController?.OnHurtAnimationEnd();
        }

        public void TriggerLightAttackProjectile()
        {
            if (_undeadMageController == null || _undeadMageController.CombatController == null) return;
            
            _undeadMageController.CombatController.FireProjectile();
        }

        public void TriggerHeavyAttackHit()
        {
            if (_undeadMageController == null || _undeadMageController.CombatController == null) return;
            
            var stats = (UndeadMageStatsSO)_undeadMageController.EnemyStats;
            _undeadMageController.CombatController.PerformMeleeAttack(stats.HeavyAttackDamage);
            _undeadMageController?.OnHeavyAttackAnimationHit();
        }
    }
}