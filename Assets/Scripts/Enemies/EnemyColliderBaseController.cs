using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : MonoBehaviour
    {
        [SerializeField] private EnemyBaseStateMachine enemyBaseStateMachine;
        
        public event Action<int, Vector3, float> OnHitStart;
        public event Action OnHitEnd;

        public void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            OnHitStart?.Invoke(damage, knockBackPosition, knockBackPower);
            enemyBaseStateMachine.HitData = new EnemyHitData(knockBackPosition, damage, knockBackPower);
        }

        public void InvokeOnHitEndEvent()
        {
            OnHitEnd?.Invoke();
        }
    }
}