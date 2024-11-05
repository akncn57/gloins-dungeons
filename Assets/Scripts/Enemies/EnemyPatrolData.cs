using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemyPatrolData
    {
        [SerializeField] public Transform PatrolCoordinate;
        [SerializeField] public bool IsCompleted;
    }
}