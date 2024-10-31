using UnityEngine;

namespace Enemies
{
    public class EnemyLineOfSight
    {
        public bool HasLineOfSight(Vector2 enemyPosition, Vector2 playerPosition, string targetTag)
        {
            var results = Physics2D.RaycastAll(enemyPosition, playerPosition - enemyPosition);
            
            foreach (var result in results)
            {
                if (result.collider != null) continue;
                
                var hasLineOfSight = result.collider.CompareTag(targetTag);

                if (hasLineOfSight)
                {
                    Debug.DrawRay(enemyPosition, playerPosition - enemyPosition, Color.green);
                    return true;
                }
            }
            
            Debug.DrawRay(enemyPosition, playerPosition - enemyPosition, Color.red);
            return false;
        }
    }
}