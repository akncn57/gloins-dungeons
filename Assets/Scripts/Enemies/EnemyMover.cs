using UnityEngine;

namespace Enemies
{
    public class EnemyMover
    {
        public void Move(Vector3 playerPosition, Rigidbody2D rigidbody, float speed)
        {
            var movement = playerPosition - rigidbody.transform.position;
            rigidbody.velocity = movement.normalized * speed;
        }

        public void Stop(Rigidbody2D rigidbody)
        {
            rigidbody.velocity = Vector2.zero;
        }
        
        public void KnockBack(Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            rigidbody.velocity = new Vector2(hitPositionX * knockBackStrength, rigidbody.velocity.y);
        }
    }
}