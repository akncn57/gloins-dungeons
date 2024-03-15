using DG.Tweening;
using HealthSystem;
using UnityEngine;

namespace CollectableItems
{
    public class CollectableItemSmallHealth : CollectableItemBase
    {
        protected override void ItemAnimation()
        {
            var sequence = DOTween.Sequence();
            var itemPosition = gameObject.transform.position;
            
            sequence.Join(gameObject.transform.DOLocalMoveY(itemPosition.y + 0.25f, AnimationSpeed)).SetEase(AnimationEase).WaitForCompletion();
            sequence.Append(gameObject.transform.DOLocalMoveY(itemPosition.y, AnimationSpeed)).SetEase(AnimationEase).WaitForCompletion();
            sequence.SetLoops(-1);
        }
        
        protected override void ItemEffect(GameObject to)
        {
            var component = to.GetComponent<HealthController>();
            if (component == null) component = to.GetComponentInParent<HealthController>();
            if (component == null) return;
            component.AddHealth((long)EffectValue);
        }
    }
}
