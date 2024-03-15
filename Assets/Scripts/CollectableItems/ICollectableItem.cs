using DG.Tweening;
using UnityEngine;

namespace CollectableItems
{
    public interface ICollectableItem
    {
        public float EffectValue { get; set; }
        public float AnimationSpeed { get; set; }
        public Ease AnimationEase { get; set; }
        
        public void Animation();
        public void OnCollect(Collider2D obj);
        public void Destroy();
    }
}
