using System;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public class GenericTimer
    {
        public event Action OnTimerStarted;
        public event Action<int> OnTimerUpdated;
        public event Action OnTimerFinished;
        
        private int _duration;
        private bool _isTimerFirstInit;

        public GenericTimer(int duration)
        {
            _duration = duration;
            _isTimerFirstInit = true;
        }

        private IEnumerator Tick()
        {
            _isTimerFirstInit = false;
            
            if (_isTimerFirstInit)
            {
                OnTimerStarted?.Invoke();
                Debug.Log("Generic Timer | Timer finished.");
            }
            
            OnTimerUpdated?.Invoke(_duration);
            yield return new WaitForSeconds(1f);
            _duration--;
            Debug.Log("Generic Timer | Timer duration is : " + _duration);

            if (_duration <= 0)
            {
                OnTimerFinished?.Invoke();
                Debug.Log("Generic Timer | Timer finished.");
            }
        }
    }
}
