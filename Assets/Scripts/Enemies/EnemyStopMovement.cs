using UnityEngine.AI;

namespace Enemies
{
    public class EnemyStopMovement
    {
        public void StopMovement(NavMeshAgent agent)
        {
            agent.isStopped = true;
        }
    }
}