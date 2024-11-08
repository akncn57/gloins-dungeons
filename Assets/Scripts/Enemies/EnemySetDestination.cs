using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemySetDestination
    {
        public void SetDestination(NavMeshAgent agent, Vector3 targetPosition)
        {
            agent.SetDestination(targetPosition);
        }
    }
}