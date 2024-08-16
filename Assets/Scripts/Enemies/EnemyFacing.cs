using UnityEngine;

namespace Enemies
{
    public class EnemyFacing
    {
        public void Facing(GameObject parentObject, float horizontalMove)
        {
            parentObject.transform.localScale = horizontalMove switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => parentObject.transform.localScale
            };
        }
    }
}