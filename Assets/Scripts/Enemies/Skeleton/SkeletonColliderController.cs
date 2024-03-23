using UnityEngine;

namespace Enemies.Skeleton
{
    [RequireComponent(typeof(Collider2D))]
    public class SkeletonColliderController : EnemyColliderBaseController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InvokeOnHitStartEvent(10);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InvokeOnHitEndEvent();
            }
        }
    }
}