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
        
        public void EndAttack()
        {
            if (_characterController != null)
            {
                _characterController.OnAttackEnd();
            }
        }
    }
}