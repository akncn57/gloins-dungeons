using UnityEngine;

namespace Enemies
{
    public class EnemyStopRigidbody
    {
        public void StopRigidbody(Rigidbody2D rigidbody)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
    }
}