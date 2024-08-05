using System.Collections.Generic;
using UnityEngine;

namespace CustomInterfaces
{
    public interface IPlayer
    {
        public List<Transform> EnemyChasePositions { get; set; }
        public GameObject GameObject { get; }
    }
}