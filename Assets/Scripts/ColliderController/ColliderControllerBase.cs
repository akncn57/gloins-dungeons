using System;
using UnityEngine;

namespace ColliderController
{
    public class ColliderControllerBase : MonoBehaviour
    {
        public event Action<int, Vector3, float> OnHitStart;
        public event Action OnHitEnd;
        
        public virtual void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            OnHitStart?.Invoke(damage, knockBackPosition, knockBackPower);
        }

        public virtual void InvokeOnHitEndEvent()
        {
            OnHitEnd?.Invoke();
        }
    }
}
