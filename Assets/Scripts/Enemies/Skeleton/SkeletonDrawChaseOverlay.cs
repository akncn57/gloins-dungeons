using CustomInterfaces;
using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonDrawChaseOverlay
    {
        public void DrawChaseOverlay(Vector3 position, float radius, SkeletonStateMachine skeletonStateMachine)
        {
            if (!skeletonStateMachine.HasLineOfSight) return;
            
            var results = Physics2D.OverlapCircleAll(position, radius);
            
            foreach (var result in results)
            {
                if (!result) continue;

                if (!result.TryGetComponent(out IPlayer player))
                    player = result.GetComponentInParent<IPlayer>();

                if (player != null)
                {
                    skeletonStateMachine.SkeletonChaseState.Init(player);
                    skeletonStateMachine.SwitchState(skeletonStateMachine.SkeletonChaseState);
                }
            }
        }
    }
}