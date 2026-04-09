using Enemies.UndeadSwordsman;
using UnityEngine;

namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanAnimationEvents : EnemyAnimationEvents
    {
        private UndeadSwordsmanController _undeadSwordsmanController;
        
        protected override void Awake()
        {
            base.Awake();
            
            // Cast down to the specific controller to access its specific methods.
            _undeadSwordsmanController = GetComponentInParent<UndeadSwordsmanController>();
        }

        public void EndLightAttack()
        {
            _undeadSwordsmanController?.OnLightAttackAnimationEnd();
        }
        
        public void EndHeavyAttack()
        {
            _undeadSwordsmanController?.OnHeavyAttackAnimationEnd();
        }

        public void EndHurt()
        {
            Debug.Log("EndHurt animation event fired by Unity!");
            if (_undeadSwordsmanController == null)
            {
                Debug.LogError("_undeadSwordsmanController is NULL! GetComponentInParent could not find the controller.");
            }
            // Then call the specific end hurt wrapper so state machine can transition out of hurt state
            _undeadSwordsmanController?.OnHurtAnimationEnd();
        }

        public void TriggerLightAttackHit()
        {
            Debug.Log($"<color=cyan>[AnimationEvent]</color> TriggerLightAttackHit fired by Unity!");
            
            if (_undeadSwordsmanController == null)
            {
                Debug.LogError("<color=cyan>[AnimationEvent]</color> _undeadSwordsmanController is null!");
                return;
            }
            if (_undeadSwordsmanController.CombatController == null)
            {
                Debug.LogError("<color=cyan>[AnimationEvent]</color> CombatController is null! Did you forget to add UndeadSwordsmanCombatController to the Game Object?");
                return;
            }
            
            var stats = (UndeadSwordsmanStatsSO)_undeadSwordsmanController.EnemyStats;
            _undeadSwordsmanController.CombatController.PerformMeleeAttack(stats.LightAttackDamage);
        }

        public void TriggerHeavyAttackHit()
        {
            if (_undeadSwordsmanController == null || _undeadSwordsmanController.CombatController == null) return;
            var stats = (UndeadSwordsmanStatsSO)_undeadSwordsmanController.EnemyStats;
            _undeadSwordsmanController.CombatController.PerformMeleeAttack(stats.HeavyAttackDamage);
        }
    }
}
