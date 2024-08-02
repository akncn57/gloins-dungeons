using Zenject;

namespace Enemies.Mage
{
    public class MageBaseState : EnemyBaseState
    {
        protected MageStateMachine MageStateMachine;
        protected IInstantiator Instantiator;

        public MageBaseState(MageStateMachine mageStateMachine, IInstantiator instantiator)
        {
            MageStateMachine = mageStateMachine;
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
