using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Base class for handling animation events sent from the Animator component.
    /// Place this script on the GameObject that contains the Animator.
    /// </summary>
    public class EnemyAnimationEvents : MonoBehaviour
    {
        protected EnemyBase EnemyController;

        protected virtual void Awake()
        {
            EnemyController = GetComponentInParent<EnemyBase>();
        }

        // Common events can be added later without being virtual if they don't need override, or handled specifically in child classes.
    }
}
