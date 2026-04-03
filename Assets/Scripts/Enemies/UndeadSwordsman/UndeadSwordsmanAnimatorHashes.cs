using UnityEngine;

namespace Enemies.UndeadSwordsman
{
    public static class UndeadSwordsmanAnimatorHashes
    {
        public static readonly int LightAttack = Animator.StringToHash("lightAttack");
        public static readonly int HeavyAttack = Animator.StringToHash("heavyAttack");
        public static readonly int Hurt = Animator.StringToHash("hurt");
        public static readonly int Death = Animator.StringToHash("death");
        public static readonly int IsWalking = Animator.StringToHash("isWalking");
    }
}