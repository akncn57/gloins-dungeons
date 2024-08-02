using Zenject;

namespace Enemies.Mage
{
    public class MageAttackBasicState : MageBaseState
    {
        public MageAttackBasicState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
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