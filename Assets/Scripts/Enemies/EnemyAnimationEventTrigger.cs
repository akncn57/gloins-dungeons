using UnityEngine;
using System;

namespace Enemies
{
    public class EnemyAnimationEventTrigger : MonoBehaviour
    {
        public event Action EnemyOnAttackBasicOverlapOpen;
        public event Action EnemyOnAttackBasicOverlapClose;
        public event Action EnemyOnAttackBasicFinished;
        public event Action EnemyOnHurtStart;
        public event Action EnemyOnHurtEnd;
        
        public void AttackBasicColliderOpen()
        {
            EnemyOnAttackBasicOverlapOpen?.Invoke();
        }
    
        public void AttackBasicColliderClose()
        {
            EnemyOnAttackBasicOverlapClose?.Invoke();
        }
        
        public void AttackBasicFinished()
        {
            EnemyOnAttackBasicFinished?.Invoke();
        }

        public void HurtStart()
        {
            EnemyOnHurtStart?.Invoke();
        }
        
        public void HurtEnd()
        {
            EnemyOnHurtEnd?.Invoke();
        }
    }
}