using UnityEngine;
using System;

namespace Enemies
{
    public class EnemyAnimationEventTrigger : MonoBehaviour
    {
        public event Action EnemyOnAttackBasicOverlapOpen;
        public event Action EnemyOnAttackBasicOverlapClose;
        public event Action EnemyOnAttackBasicFinished;
        public event Action EnemyOnAttackHeavyOverlapOpen;
        public event Action EnemyOnAttackHeavyOverlapClose;
        public event Action EnemyOnAttackHeavyFinished;
        public event Action EnemyOnHurtStart;
        public event Action EnemyOnHurtEnd;
        public event Action EnemyDeath;
        
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
        
        public void AttackHeavyColliderOpen()
        {
            EnemyOnAttackHeavyOverlapOpen?.Invoke();
        }
    
        public void AttackHeavyColliderClose()
        {
            EnemyOnAttackHeavyOverlapClose?.Invoke();
        }
        
        public void AttackHeavyFinished()
        {
            EnemyOnAttackHeavyFinished?.Invoke();
        }

        public void HurtStart()
        {
            EnemyOnHurtStart?.Invoke();
        }
        
        public void HurtEnd()
        {
            EnemyOnHurtEnd?.Invoke();
        }

        public void Death()
        {
            EnemyDeath?.Invoke();
        }
    }
}