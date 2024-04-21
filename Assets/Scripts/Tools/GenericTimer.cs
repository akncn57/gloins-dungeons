using System;
using System.Collections;
using UnityEngine;
using UtilScripts;

namespace Tools
{
    public class GenericTimer
    {
        public event Action OnTimerStarted;
        public event Action<int> OnTimerUpdated;
        public event Action OnTimerFinished;

        private int _duration;
        private bool _isTimerFirstInit;

        public GenericTimer(int duration, CoroutineRunner coroutineRunner)
        {
            _duration = duration;
            _isTimerFirstInit = true;
            coroutineRunner.TryStartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            if (_isTimerFirstInit)
            {
                OnTimerStarted?.Invoke();
                _isTimerFirstInit = false;
                
                //Debug.Log("Generic Timer | Timer started.");
            }

            //Debug.Log("Generic Timer | Timer duration is : " + _duration);
            
            while (_duration > 0)
            {
                OnTimerUpdated?.Invoke(_duration);
                yield return new WaitForSeconds(1f);
                _duration--;
                
                //Debug.Log("Generic Timer | Timer duration is : " + _duration);
            }

            OnTimerFinished?.Invoke();
            
            //Debug.Log("Generic Timer | Timer finished.");
        }
    }
}
