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

        // --- Combat Trigger Events --- //
        
        public void TriggerLightAttackHit()
        {
            // Placeholder for when you add a CombatController or damage dealer script to UndeadSwordsman
            // Example: _undeadSwordsmanController.CombatController.PerformMeleeAttack(LightDamage);
        }

        public void TriggerHeavyAttackHit()
        {
            // Similar to Light Attack Hit
        }
    }
}
