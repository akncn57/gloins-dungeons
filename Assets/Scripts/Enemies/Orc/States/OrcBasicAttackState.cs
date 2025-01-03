using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcBasicAttackState : OrcBaseState
    {
        private static readonly int BasicAttackAnimationHash = Animator.StringToHash("AttackBasic_BlendTree");
        
        protected OrcBasicAttackState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            OrcStateMachine.Animator.Play(BasicAttackAnimationHash);
            
            var attackHorizontal = OrcStateMachine.Animator.GetFloat("LastHorizontal");
            var attackVertical = OrcStateMachine.Animator.GetFloat("LastVertical");
            
            if (Mathf.Approximately(attackHorizontal, 1))
            {
                Debug.Log("Orc attack Right!");
                
            }
            else if (Mathf.Approximately(attackHorizontal, -1))
            {
                Debug.Log("Orc attack Left!");
                
            }
            else if (Mathf.Approximately(attackVertical, 1))
            {
                Debug.Log("Orc attack Up!");
                
            }
            else if (Mathf.Approximately(attackVertical, -1))
            {
                Debug.Log("Orc attack Down!");
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