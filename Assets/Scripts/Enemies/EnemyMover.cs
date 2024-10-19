using UnityEngine;

namespace Enemies
{
    public class EnemyMover
    {
        public void Move(Vector3 playerPosition, Rigidbody2D rigidbody, float speed)
        {
            var movement = playerPosition - rigidbody.transform.position;
            rigidbody.linearVelocity = movement.normalized * speed;
        }

        public void Stop(Rigidbody2D rigidbody)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
        
        public void KnockBack(Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            rigidbody.linearVelocity = new Vector2(hitPositionX * knockBackStrength, rigidbody.linearVelocity.y);
        }
    }
}