using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools
{
    public class DrawEntityGizmos : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private GizmosTypes type;

        [ShowIf("type", GizmosTypes.Cube)]
        [SerializeField] private Vector3 center;
        [ShowIf("type", GizmosTypes.Cube)]
        [SerializeField] private Vector3 size;
        
        private void OnDrawGizmos()
        {
            // switch (type)
            // {
            //     case GizmosTypes.Cube:
            //         Gizmos.DrawCube();
            // }
        }
    }

    public enum GizmosTypes
    {
        Cube,
        Sphere,
        Line
    }
}