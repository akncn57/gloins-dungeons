using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : MonoBehaviour
    {
        public event Action<int> OnHitStart;
        public event Action OnHitEnd;

        public void InvokeOnHitStartEvent(int damage)
        {
            OnHitStart?.Invoke(damage);
        }

        public void InvokeOnHitEndEvent()
        {
            OnHitEnd?.Invoke();
        }
    }
}