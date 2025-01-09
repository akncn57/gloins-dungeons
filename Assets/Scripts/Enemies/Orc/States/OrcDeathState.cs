using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcDeathState : OrcBaseState
    {
        protected OrcDeathState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
        
        public override void OnEnter()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyDeath += CheckDeathAnimationFinished;
            
            OrcStateMachine.Animator.Play("Death-BlendTree");
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyDeath -= CheckDeathAnimationFinished;
        }

        private void CheckDeathAnimationFinished()
        {
            OrcStateMachine.gameObject.SetActive(false);
        }
    }
}