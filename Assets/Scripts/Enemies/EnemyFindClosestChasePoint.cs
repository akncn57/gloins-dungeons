using UnityEngine;

namespace Enemies
{
    public class EnemyFindClosestChasePoint
    {
        public Vector3 GetClosestChasePoint(Vector3 position, Vector3 playerPosition, float offset)
        {
            return position.x <= playerPosition.x 
                ? new Vector3(playerPosition.x - offset, playerPosition.y, 0f) 
                : new Vector3(playerPosition.x + offset, playerPosition.y, 0f);
        }
    }
}