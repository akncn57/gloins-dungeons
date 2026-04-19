using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [CreateAssetMenu(fileName = "NewCharacterSounds", menuName = "Game Data/Character Sounds")]
    public class CharacterSoundsSo : ScriptableObject
    {
        public AudioClip[] walkSounds;
        public AudioClip lightAttackSound;
        public AudioClip heavyAttackSound;
        public AudioClip dashSound;
        public AudioClip hurtSound;
        public AudioClip deathSound;
    }
}