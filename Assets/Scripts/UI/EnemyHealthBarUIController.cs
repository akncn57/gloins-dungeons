using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals.Combat;

namespace UI
{
    public class EnemyHealthBarUIController : MonoBehaviour
    {
        [SerializeField] private GameObject healthBarParent;
        [SerializeField] private TMP_Text enemyNameText;
        [SerializeField] private Image fillImage;
        
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Start()
        {
            _signalBus.Subscribe<TargetChangedSignal>(OnTargetChanged);
            _signalBus.Subscribe<HealthChangedSignal>(OnHealthChanged);
        }
        
        public void OnDisable()
        {
            _signalBus.Unsubscribe<TargetChangedSignal>(OnTargetChanged);
            _signalBus.Unsubscribe<HealthChangedSignal>(OnHealthChanged);
        }

        private void OnTargetChanged(TargetChangedSignal signal)
        {
            Debug.Log("AKO SIGNAL FIRED!");
            healthBarParent.SetActive(signal.HasTarget);

            enemyNameText.text = signal.EnemyName;
            
            var fillValue = signal.CurrentHealth / signal.MaxHealth;
            fillImage.fillAmount = fillValue;
        }
        
        private void OnHealthChanged(HealthChangedSignal signal)
        {
            if (healthBarParent.activeSelf)
            {
                var fillValue = signal.CurrentHealth / signal.MaxHealth;
                fillImage.DOFillAmount(fillValue, 0.2f);
            }
        }
    }
}