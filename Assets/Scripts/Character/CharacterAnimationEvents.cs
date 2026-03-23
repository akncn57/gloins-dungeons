using UnityEngine;

namespace Character
{
    public class CharacterAnimationEvents : MonoBehaviour
    {
        private CharacterController _characterController;
        
        private void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
        }
        
        public void EndLightAttack()
        {
            _characterController.OnLightAttackAnimationEnd();
        }
        
        public void EndHeavyAttack()
        {
            _characterController.OnHeavyAttackAnimationEnd();
        }
    }
}