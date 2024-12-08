using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMover
    {
        public void Move(Rigidbody2D rigidbody, Vector2 movement, float speed)
        {
            movement = movement.normalized * speed;
            rigidbody.linearVelocity = movement;
        }

        public void Stop(Rigidbody2D rigidbody)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }

        public void KnockBack(Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            rigidbody.linearVelocity = new Vector2(hitPositionX * knockBackStrength, rigidbody.linearVelocity.y);
        }

        public void Dash(Rigidbody2D rigidbody, Vector2 dashDirection, float force)
        {
            rigidbody.AddForce(dashDirection * force, ForceMode2D.Impulse);
        }
        
        // public IEnumerator DashCor(Rigidbody2D rigidbody, Vector2 dashDirection, float dashForce, float dashDuration)
        // {
        //     var elapsed = 0f;
        //     
        //     while (elapsed < dashDuration)
        //     {
        //         rigidbody.AddForce(dashDirection * dashForce * Time.deltaTime, ForceMode2D.Force);
        //         elapsed += Time.deltaTime;
        //         yield return null;
        //     }
        // }
    }
}