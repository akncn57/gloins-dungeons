using Character.Combat;
using UnityEngine;

namespace Character
{
    public class CharacterAnimationEvents : MonoBehaviour
    {
        private CharacterController _characterController;
        private CharacterAudioController _characterAudioController;
        private CharacterCombatController _characterCombatController;
        
        private void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
            _characterAudioController = GetComponentInParent<CharacterAudioController>();
            _characterCombatController = GetComponentInParent<CharacterCombatController>();
        }
        
        public void EndLightAttack()
        {
            _characterController.OnLightAttackAnimationEnd();
        }
        
        public void EndHeavyAttack()
        {
            _characterController.OnHeavyAttackAnimationEnd();
        }

        public void EndHurt()
        {
            _characterController.OnHurtAnimationEnd();
        }

        private void PlayLightAttackSound()
        {
            _characterAudioController.PlayLightAttack();
        }
        
        private void PlayHeavyAttackSound()
        {
            _characterAudioController.PlayHeavyAttack();
        }

        private void PlayWalkSound()
        {
            _characterAudioController.PlayWalk();
        }
        
        public void TriggerLightAttackHit()
        {
            _characterCombatController?.PerformMeleeAttack(10 /* Veya _characterController.Stats.LightAttackDamage */);
        }
        
        public void TriggerHeavyAttackHit()
        {
            _characterCombatController?.PerformMeleeAttack(25 /* Ağır hasar */);
        }
    }
}