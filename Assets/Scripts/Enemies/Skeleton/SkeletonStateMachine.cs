using System;

namespace Enemies.Skeleton
{
    public class SkeletonStateMachine : EnemyBaseStateMachine
    {
        private void Start()
        {
            SwitchState(new SkeletonIdleState(this));
        }
    }
}