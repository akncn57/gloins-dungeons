using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcBasicAttackState : OrcBaseState
    {
        protected OrcBasicAttackState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            var attackHorizontal = Mathf.Abs(OrcStateMachine.Animator.GetFloat("LastHorizontal") - 1) < Mathf.Abs(OrcStateMachine.Animator.GetFloat("LastHorizontal") + 1) ? 1 : -1;
            var attackVertical = Mathf.Abs(OrcStateMachine.Animator.GetFloat("LastVertical") - 1) < Mathf.Abs(OrcStateMachine.Animator.GetFloat("LastVertical") + 1) ? 1 : -1;
            
            if (Mathf.Approximately(attackHorizontal, 1))
            {
                Debug.Log("Orc attack Right!");
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
            }
            else if (Mathf.Approximately(attackHorizontal, -1))
            {
                Debug.Log("Orc attack Left!");
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
            }
            else if (Mathf.Approximately(attackVertical, 1))
            {
                Debug.Log("Orc attack Up!");
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
            }
            else if (Mathf.Approximately(attackVertical, -1))
            {
                Debug.Log("Orc attack Down!");
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
            }
            else
            {
                Debug.Log("HEHEHE!!");
            }
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}