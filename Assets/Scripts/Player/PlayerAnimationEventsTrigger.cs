using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationEventsTrigger : MonoBehaviour
    {
        public event Action PlayerOnAttackBasicOverlapOpen;
        public event Action PlayerOnAttackBasicOverlapClose;
        public event Action PlayerOnAttackBasicFinished;
        public event Action PlayerOnHurtStart;
        public event Action PlayerOnHurtEnd;

        public void AttackBasicColliderOpen()
        {
            PlayerOnAttackBasicOverlapOpen?.Invoke();
        }
    
        public void AttackBasicColliderClose()
        {
            PlayerOnAttackBasicOverlapClose?.Invoke();
        }
        
        public void AttackBasicFinished()
        {
            PlayerOnAttackBasicFinished?.Invoke();
        }

        public void HurtStart()
        {
            PlayerOnHurtStart?.Invoke();
        }
        
        public void HurtEnd()
        {
            PlayerOnHurtEnd?.Invoke();
        }
    }
}
