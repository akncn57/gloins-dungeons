using CustomInterfaces;
using Enemies.Skeleton;
using UnityEngine;

namespace Enemies
{
    public class EnemyDrawChaseOverlay
    {
        public bool IsPlayerWithinRadius(Vector3 position, float radius, EnemyBaseStateMachine enemyBaseStateMachine)
        {
            var results = Physics2D.OverlapCircleAll(position, radius);
            
            foreach (var result in results)
            {
                if (!result) continue;

                if (!result.TryGetComponent(out IPlayer player))
                    player = result.GetComponentInParent<IPlayer>();

                if (player != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}