using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonBaseState : EnemyBaseState
    {
        protected SkeletonStateMachine SkeletonStateMachine;
        protected IInstantiator Instantiator;
        
        protected SkeletonBaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator)
        {
            SkeletonStateMachine = skeletonStateMachine;
            Instantiator = instantiator;
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