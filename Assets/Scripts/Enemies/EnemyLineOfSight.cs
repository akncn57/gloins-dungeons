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
                Debug.Log("Hit to " + result.transform.name);
                
                var hasLineOfSight = result.collider.CompareTag(targetTag);

                if (hasLineOfSight)
                {
                    Debug.Log("Ray Hit Player!");
                    Debug.DrawRay(enemyPosition, playerPosition - enemyPosition, Color.green);
                    return true;
                }
                else
                {
                    Debug.Log("Ray didn't Hit Player!");
                    Debug.DrawRay(enemyPosition, playerPosition - enemyPosition, Color.red);
                    return false;
                }
            }
            
            return false;
        }
    }
}