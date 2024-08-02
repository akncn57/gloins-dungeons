using CustomInterfaces;
using Zenject;

namespace Enemies.Mage
{
    public class MageChaseState : MageBaseState
    {
        public MageChaseState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
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
        
        public void Init(IPlayer player)
        {
            //_enemyChasePositionsList = player.EnemyChasePositions;
        }
    }
}