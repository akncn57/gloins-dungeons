using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : MonoBehaviour
    {
        public event Action<int> OnHitStart;
        public event Action OnHitEnd;

        protected void InvokeOnHitStartEvent(int damage)
        {
            OnHitStart?.Invoke(damage);
        }

        protected void InvokeOnHitEndEvent()
        {
            OnHitEnd?.Invoke();
        }
    }
}