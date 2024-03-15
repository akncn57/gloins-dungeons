using DG.Tweening;
using UnityEngine;

namespace CollectableItems
{
    public abstract class CollectableItemBase : MonoBehaviour, ICollectableItem
    {
        [SerializeField] protected float effectValue;
        [SerializeField] protected float animationSpeed;
        [SerializeField] protected Ease animationEase;
        
        public float EffectValue
        {
            get => effectValue;
            set => effectValue = value;
        }
        
        public float AnimationSpeed
        {
            get => animationSpeed;
            set => animationSpeed = value;
        }
        
        public Ease AnimationEase
        {
            get => animationEase;
            set => animationEase = value;
        }
        
        private void OnEnable()
        {
            Animation();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollect(other);
        }
        
        public void Animation()
        {
            ItemAnimation();
        }

        public void OnCollect(Collider2D obj)
        {
            ItemEffect(obj.gameObject);
            Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        
        protected abstract void ItemEffect(GameObject to);
        protected abstract void ItemAnimation();
    }
}