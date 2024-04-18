namespace Enemies.Skeleton
{
    public class SkeletonBaseState : EnemyBaseState
    {
        protected SkeletonStateMachine SkeletonStateMachine;
        
        protected SkeletonBaseState(SkeletonStateMachine SkeletonStateMachine)
        {
            this.SkeletonStateMachine = SkeletonStateMachine;
        }

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnTick()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}