using UnityEngine;

namespace Player
{
    public class PlayerMover
    {
        public void Move(Rigidbody2D rigidbody, Vector2 movement, float speed)
        {
            movement = movement.normalized * speed;
            rigidbody.velocity = movement;
        }

        public void Stop(Rigidbody2D rigidbody)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}