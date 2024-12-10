using UnityEngine;

namespace Enemies
{
    public class EnemyLineOfSight
    {
        public bool HasLineOfSight(Collider2D enemyCollider, Collider2D playerCollider, string targetTag, LayerMask layerMask)
        {
            var results = new RaycastHit2D[10];
            var enemyOrigin = (Vector2)enemyCollider.transform.position + enemyCollider.offset;
            var playerOrigin = (Vector2)playerCollider.transform.position + playerCollider.offset;
            
            Physics2D.RaycastNonAlloc(enemyOrigin, playerOrigin - enemyOrigin, results, 100f, layerMask);
            
            foreach (var result in results)
            {
                Debug.Log("Hit to " + result.transform.name);
                
                var hasLineOfSight = result.transform.gameObject.CompareTag(targetTag);

                if (hasLineOfSight)
                {
                    Debug.Log("Ray Hit Player!");
                    Debug.DrawRay(enemyOrigin, playerOrigin - enemyOrigin, Color.green);
                    return true;
                }
                else if (result.collider != enemyCollider)
                {
                    Debug.Log("Ray didn't Hit Player!");
                    Debug.DrawRay(enemyOrigin, playerOrigin - enemyOrigin, Color.red);
                    return false;
                }
            }
            
            return false;
        }
    }
}