using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : MonoBehaviour
    {
        public event Action<int, Vector3, float> OnHitStart;
        public event Action OnHitEnd;

        public void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            OnHitStart?.Invoke(damage, knockBackPosition, knockBackPower);
        }

        public void InvokeOnHitEndEvent()
        {
            OnHitEnd?.Invoke();
        }
    }
}