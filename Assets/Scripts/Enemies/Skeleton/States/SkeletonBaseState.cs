using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonBaseState : EnemyBaseState
    {
        protected SkeletonStateMachine SkeletonStateMachine;
        protected IInstantiator Instantiator;
        protected SignalBus SignalBus;
        
        protected SkeletonBaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator, SignalBus signalBus)
        {
            SkeletonStateMachine = skeletonStateMachine;
            Instantiator = instantiator;
            SignalBus = signalBus;
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