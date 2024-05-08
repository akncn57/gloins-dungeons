using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemyPatrolData
    {
        public Transform PatrolCoordinate;
        public bool IsCompleted;
    }
}