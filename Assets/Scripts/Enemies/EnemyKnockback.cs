using UnityEngine;

namespace Enemies
{
    public class EnemyKnockback
    {
        public void KnockBack(Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            rigidbody.linearVelocity = new Vector2(hitPositionX * knockBackStrength, rigidbody.linearVelocity.y);
        }
    }
}