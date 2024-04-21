using System;
using System.Collections;
using UnityEngine;

namespace UtilScripts
{
    public class CoroutineRunner : MonoBehaviour
    {
        public Coroutine TryStartCoroutine(IEnumerator coroutine)
        {
            try
            {
                return StartCoroutine(coroutine);
            }
            catch (Exception e)
            {
                Debug.Log("Coroutine start exception : " + e);
                return null;
            }
        }
    }
}
