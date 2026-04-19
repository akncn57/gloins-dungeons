using UnityEngine;

namespace Utils
{
    public class DisableAfterAnimation : MonoBehaviour
    {
        public void DisableObject()
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}