using InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerDirectionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D basicAttackRightCollider;
        [SerializeField] private Collider2D basicAttackLeftCollider;
        [SerializeField] private Collider2D basicAttackUpCollider;
        [SerializeField] private Collider2D basicAttackDownCollider;
        
        public Collider2D GetBasicAttackCollider()
        {
            if (Mathf.Approximately(animator.GetFloat("LastHorizontal"), 1)) return basicAttackRightCollider;
            if (Mathf.Approximately(animator.GetFloat("LastHorizontal"), -1)) return basicAttackLeftCollider;
            if (Mathf.Approximately(animator.GetFloat("LastVertical"), 1)) return basicAttackUpCollider;
            if (Mathf.Approximately(animator.GetFloat("LastVertical"), -1)) return basicAttackDownCollider;
            return basicAttackDownCollider;
        }
    }
}
