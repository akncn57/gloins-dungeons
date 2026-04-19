using UnityEngine;

namespace Character
{
    public static class CharacterAnimatorHashes
    {
        public static readonly int IsMoving = Animator.StringToHash("isMoving");
        public static readonly int LightAttack = Animator.StringToHash("lightAttack");
        public static readonly int HeavyAttack = Animator.StringToHash("heavyAttack");
        public static readonly int Hurt = Animator.StringToHash("hurt");
        public static readonly int Death = Animator.StringToHash("death");
    }
}