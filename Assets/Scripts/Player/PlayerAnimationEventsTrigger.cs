using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationEventsTrigger : MonoBehaviour
    {
        public event Action OnAttackBasicColliderOpen;
        public event Action OnAttackBasicColliderClose;
        public event Action OnAttackBasicFinished;
        public event Action OnHurtStart;
        public event Action OnHurtEnd;

        public void AttackBasicColliderOpen()
        {
            OnAttackBasicColliderOpen?.Invoke();
        }
    
        public void AttackBasicColliderClose()
        {
            OnAttackBasicColliderClose?.Invoke();
        }
        
        public void AttackBasicFinished()
        {
            OnAttackBasicFinished?.Invoke();
        }

        public void HurtStart()
        {
            OnHurtStart?.Invoke();
        }
        
        public void HurtEnd()
        {
            OnHurtEnd?.Invoke();
        }
    }
}
