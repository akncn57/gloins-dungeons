using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyStopMovement
    {
        public void StopMovement(NavMeshAgent agent)
        {
            agent.isStopped = true;
            agent.stoppingDistance = 0f;
            agent.velocity = Vector3.zero;
        }
    }
}