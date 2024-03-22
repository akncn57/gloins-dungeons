using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : MonoBehaviour
    {
        public event Action<int> OnHitStart;

        protected void InvokeOnHitStartEvent(int damage)
        {
            Debug.Log("sa");
            OnHitStart?.Invoke(damage);
        }
    }
}