using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationEventsTrigger : MonoBehaviour
    {
        public event Action OnAttackBasicColliderOpen;
        public event Action OnAttackBasicColliderClose;

        public void AttackBasicColliderOpen()
        {
            OnAttackBasicColliderOpen?.Invoke();
        }
    
        public void AttackBasicColliderClose()
        {
            OnAttackBasicColliderClose?.Invoke();
        }
    }
}
